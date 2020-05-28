using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DotNetCore.Data.Entity;
using DotNetCore.Utility;
using DotNetCore.Web.Models;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web.Infrastructure.Utility
{
    public class ManualConvertData
    {
        private readonly IContentService _contentService;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;
        private readonly IRoutingService _routingService;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;
        public ManualConvertData(IContentService contentService, IMapper mapper, IAuthorService authorService,
            ICategoryService categoryService, IRoutingService routingService, ITagService tagService)
        {
            _contentService = contentService;
            _authorService = authorService;
            _categoryService = categoryService;
            _routingService = routingService;
            _tagService = tagService;
            _mapper = mapper;
        }

        public CategoryViewModel ConvertCategoryViewModel(Category category)
        {
            var viewModel = new CategoryViewModel();
            viewModel = _mapper.Map<CategoryViewModel>(category);
            var categoryRouting = _routingService.GetDefineRouting(Constants.CategoryController, Constants.CategoryAction, category.CategoryId);
            viewModel.Url = categoryRouting != null ? string.Format("/{0}/{1}", categoryRouting.FriendlyUrlLv1, categoryRouting.FriendlyUrlLv2) : string.Empty;
            return viewModel;
        }

        public CategorySimpleViewModel ConvertSimpleCategoryViewModel(Category category)
        {
            var viewModel = new CategorySimpleViewModel();
            viewModel = _mapper.Map<CategorySimpleViewModel>(category);
            var categoryRouting = _routingService.GetDefineRouting(Constants.CategoryController, Constants.CategoryAction, category.CategoryId);
            viewModel.Url = categoryRouting != null ? string.Format("/{0}/{1}", categoryRouting.FriendlyUrlLv1, categoryRouting.FriendlyUrlLv2) : string.Empty;
            return viewModel;
        }

        public TagViewModel ConvertTagViewModel(Tag tag)
        {
            var viewModel = new TagViewModel();
            viewModel = _mapper.Map<TagViewModel>(tag);
            var tagRouting = _routingService.GetDefineRouting(Constants.TagController, Constants.TagAction, tag.TagId);
            viewModel.TagUrl = tagRouting != null ? string.Format("/{0}/{1}", tagRouting.FriendlyUrlLv1, tagRouting.FriendlyUrlLv2) : string.Empty;
            return viewModel;
        }

        public AuthorViewModel ConvertAuthorViewModel(Author author)
        {
            var viewModel = new AuthorViewModel();
            viewModel = _mapper.Map<AuthorViewModel>(author);
            var authorRouting = _routingService.GetDefineRouting(Constants.AuthorController, Constants.AuthorAction, author.AuthorId);
            viewModel.Url = authorRouting != null ? string.Format("/{0}/{1}", authorRouting.FriendlyUrlLv1, authorRouting.FriendlyUrlLv2) : string.Empty;
            return viewModel;
        }

        public async System.Threading.Tasks.Task<ContentViewModel> ConvertContentViewModelAsync(Content content)
        {
            var viewModel = new ContentViewModel();
            viewModel = _mapper.Map<ContentViewModel>(content);
            var contentRouting = _routingService.GetDefineRouting(Constants.ContentController, Constants.ContentAction, content.ContentId);
            viewModel.Url = contentRouting != null ? string.Format("/{0}/{1}", contentRouting.FriendlyUrlLv1, contentRouting.FriendlyUrlLv2) : string.Empty;
            viewModel.DatePublish = _contentService.GetDatePublishByContent(content.ContentId).ToString("dd-MMM-yyyy");
            var author = await _authorService.GetAuthorById(content.AuthorId);
            if (author != null)
            {
                viewModel.Author = ConvertAuthorViewModel(author);
            }
            var tags = _tagService.GetTagByContent(content.ContentId);
            if (tags != null && tags.Any())
            {
                viewModel.Tags = new List<TagViewModel>();
                foreach (var tag in tags)
                {
                    var model = ConvertTagViewModel(tag);
                    viewModel.Tags.Add(model);
                }
            }
            var categories = _categoryService.GetCategoriesByContent(content.ContentId);
            if (categories != null && categories.Any())
            {
                viewModel.ListCategoryOfContent = new List<CategorySimpleViewModel>();
                foreach (var category in categories)
                {
                    var model = ConvertSimpleCategoryViewModel(category);
                    viewModel.ListCategoryOfContent.Add(model);
                }
            }
            return viewModel;
        }

    }
}
