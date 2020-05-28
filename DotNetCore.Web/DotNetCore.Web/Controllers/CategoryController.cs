using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using DotNetCore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Data.Entity;
using System.Threading.Tasks;
using DotNetCore.Utility;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IContentService _contentService;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;
        private readonly IRoutingService _routingService;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;
        public CategoryController(IContentService contentService, IMapper mapper, IAuthorService authorService,
            ICategoryService categoryService, IRoutingService routingService, ITagService tagService)
        {
            _contentService = contentService;
            _authorService = authorService;
            _categoryService = categoryService;
            _routingService = routingService;
            _tagService = tagService;
            _mapper = mapper;
        }

        // GET: Category
        public async System.Threading.Tasks.Task<ActionResult> LoadCategoryAsync(int? id)
        {
            if(id.HasValue)
            {
                var viewModel = new CategoryViewModel();
                var category = await _categoryService.GetCategoryById(id.Value);
                if(category != null)
                {
                    viewModel = _mapper.Map<CategoryViewModel>(category);
                    var listContentByCategory = _contentService.GetListContentsByCategory(id.Value,Utility.Common.ContentStatus.Published);
                    if(listContentByCategory != null && listContentByCategory.Any())
                    {
                        viewModel.NewestPost = await ConvertToViewModelAsync(listContentByCategory.FirstOrDefault());
                        if(listContentByCategory.Count() > 1)
                        {
                            viewModel.ListLastestPosts = new List<ContentViewModel>();
                            foreach (var content in listContentByCategory.Skip(1).Take(4))
                            {
                                var model = await ConvertToViewModelAsync(content);
                                viewModel.ListLastestPosts.Add(model);
                            }
                        }
                    }
                    return View("LoadCategory",viewModel);
                }
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