using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Utility;
using DotNetCore.Web.Models;
using AutoMapper;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web
{
    [ViewComponent(Name = "ListCategory")]
    public class ListCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IContentService _contentService;
        private readonly IRoutingService _routingService;
        private readonly IMapper _mapper;
        public ListCategoryViewComponent(ICategoryService categoryService, IMapper mapper, IContentService contentService,
            IRoutingService routingService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _contentService = contentService;
            _routingService = routingService;
        }
        public IViewComponentResult Invoke(Common.ListCategoryPosition listCategoryPosition)
        {
            var categories = _categoryService.GetAllCategoryParent();
            if (categories != null)
            {
                var viewModel = new List<CategorySimpleViewModel>();
                foreach (var category in categories)
                {
                    if (_contentService.IsCategoryHasContent(category.CategoryId))
                    {
                        var model = _mapper.Map<CategorySimpleViewModel>(category);
                        model.Url = _routingService.GetUrl(Common.RoutingType.Category, category.CategoryId);
                        model.PostTotals = _contentService.GetPostTotalForCategory(category.CategoryId);
                        viewModel.Add(model);
                    }
                }
                var viewName = listCategoryPosition == Common.ListCategoryPosition.Body ? "ListCategoryBody" : "ListCategoryFooter";
                return View(viewName, viewModel);
            }
            return null;
        }
    }
}
