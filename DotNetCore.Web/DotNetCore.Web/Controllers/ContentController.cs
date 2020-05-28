using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Web.Models;
using System.Threading.Tasks;
using DotNetCore.Data.Entity;
using DotNetCore.Utility;
using DotNetCore.Service.Interface;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Authorization;

namespace DotNetCore.Web.Controllers
{
    public class ContentController : BaseController
    {
        private readonly IContentService _contentService;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;
        private readonly IRoutingService _routingService;
        private readonly ITagService _tagService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public ContentController(IContentService contentService, IMapper mapper, IAuthorService authorService,
            ICategoryService categoryService, IRoutingService routingService, ITagService tagService, ICommentService commentService)
        {
            _contentService = contentService;
            _authorService = authorService;
            _categoryService = categoryService;
            _routingService = routingService;
            _tagService = tagService;
            _commentService = commentService;
            _mapper = mapper;
        }
        public async System.Threading.Tasks.Task<ActionResult> DetailAsync(int? id)
        {
            if (id.HasValue)
            {
                var content = await _contentService.GetContentById(id.Value);
                if (content != null)
                {
                    _contentService.IncreaseCounterContent(content);
                    var viewModel = await ConvertToViewModelAsync(content);
                    viewModel.TotalComments = await _commentService.GetTotalCommentByContentAsync(id.Value);
                    return View("Detail",viewModel);
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SearchAsync(string search)
        {
            var viewModel = new List<ContentViewModel>();
            if (string.IsNullOrEmpty(search))
            {
                var contents =  await _contentService.SearchContent(search);
                if(contents != null && contents.Any())
                {
                    foreach (var content in contents)
                    {
                        var model = await ConvertToViewModelAsync(content);
                        viewModel.Add(model);
                    }
                }
            }
            var result = PagingList.Create(viewModel, 10, 1);
            return View("Search",result);
        }
    }
}