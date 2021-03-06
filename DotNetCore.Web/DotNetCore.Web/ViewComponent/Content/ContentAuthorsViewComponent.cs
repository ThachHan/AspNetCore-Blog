﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Utility;
using DotNetCore.Web.Models;
using AutoMapper;
using ReflectionIT.Mvc.Paging;
using DotNetCore.Data.Entity;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web
{
    [ViewComponent(Name = "ContentAuthors")]
    public class ContentAuthorsViewComponent : ViewComponent
    {
        private readonly IContentService _contentService;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;
        private readonly IRoutingService _routingService;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;
        public ContentAuthorsViewComponent(IContentService contentService, IMapper mapper, IAuthorService authorService,
            ICategoryService categoryService, IRoutingService routingService, ITagService tagService)
        {
            _contentService = contentService;
            _authorService = authorService;
            _categoryService = categoryService;
            _routingService = routingService;
            _tagService = tagService;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? authorId, int page = 1)
        {
            if (authorId.HasValue)
            {
                var listContent = new List<ContentViewModel>();
                var listContentofAuthor = _contentService.GetListContentOfAuthor(authorId.Value, Utility.Common.ContentStatus.Published);
                if (listContentofAuthor != null && listContentofAuthor.Any())
                {
                    foreach (var content in listContentofAuthor)
                    {
                        var model = await ConvertToViewModelAsync(content);
                        listContent.Add(model);
                    }
                    var viewModel = PagingList.Create(listContent, 10, page);
                    return View(viewModel);
                }
                return View();
            }
            return null;
        }


        private async Task<ContentViewModel> ConvertToViewModelAsync(Content content)
        {
            var viewModel = _mapper.Map<ContentViewModel>(content);
            var contentRouting = _routingService.GetDefineRouting(Constants.ContentController, Constants.ContentAction, content.ContentId);
            viewModel.Url = contentRouting != null ? string.Format("/{0}/{1}", contentRouting.FriendlyUrlLv1, contentRouting.FriendlyUrlLv2) : string.Empty;
            viewModel.DatePublish = _contentService.GetDatePublishByContent(content.ContentId).ToString("dd-MMM-yyyy");
            var author = await _authorService.GetAuthorById(content.AuthorId);
            if (author != null)
            {
                viewModel.Author = _mapper.Map<AuthorViewModel>(author);
                var authorRouting = _routingService.GetDefineRouting(Constants.AuthorController, Constants.AuthorAction, author.AuthorId);
                viewModel.Author.Url = authorRouting != null ? string.Format("/{0}/{1}", authorRouting.FriendlyUrlLv1, authorRouting.FriendlyUrlLv2) : string.Empty;
            }
            var tags = _tagService.GetTagByContent(content.ContentId);
            if (tags != null && tags.Any())
            {
                viewModel.Tags = new List<TagViewModel>();
                foreach (var tag in tags)
                {
                    var model = _mapper.Map<TagViewModel>(tag);
                    var tagRouting = _routingService.GetDefineRouting(Constants.TagController, Constants.TagAction, tag.TagId);
                    model.TagUrl = tagRouting != null ? string.Format("/{0}/{1}", tagRouting.FriendlyUrlLv1, tagRouting.FriendlyUrlLv2) : string.Empty;
                    viewModel.Tags.Add(model);
                }
            }
            var categories = _categoryService.GetCategoriesByContent(content.ContentId);
            if (categories != null && categories.Any())
            {
                viewModel.ListCategoryOfContent = new List<CategorySimpleViewModel>();
                foreach (var category in categories)
                {
                    var model = _mapper.Map<CategorySimpleViewModel>(category);
                    var categoryRouting = _routingService.GetDefineRouting(Constants.CategoryController, Constants.CategoryAction, category.CategoryId);
                    model.Url = categoryRouting != null ? string.Format("/{0}/{1}", categoryRouting.FriendlyUrlLv1, categoryRouting.FriendlyUrlLv2) : string.Empty;
                    viewModel.ListCategoryOfContent.Add(model);
                }
            }
            return viewModel;
        }
    }
}
