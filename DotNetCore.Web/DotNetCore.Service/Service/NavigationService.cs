using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DotNetCore.Data.Repositories;
using DotNetCore.Data.Models;
using DotNetCore.Data.Entity;
using DotNetCore.Utility;
using DotNetCore.Service.Interface;
using DotNetCore.Data.Interface;
namespace DotNetCore.Service
{
    public class NavigationService : INavigationService
    {
        private readonly ICategoryService _categoryService;
        private readonly IRoutingService _routingService;

        public NavigationService(ICategoryService CategoryService, IRoutingService routingService)
        {
            _categoryService = CategoryService;
            _routingService = routingService;
        }

        public IEnumerable<NavigationItem> GetNavigationParents()
        {
            var listCategory = _categoryService.GetAllCategoryParent();
            if (listCategory != null && listCategory.Any())
            {
                return ConvertCategoryToNavigation(listCategory);
            }
            return null;
        }

        public IList<NavigationItem> GetNavigationChilds(int parentId, Common.NavigationPosition navigationPosition)
        {
            var listCategory = _categoryService.GetAllCategorysForParentAndPosition(parentId, navigationPosition);
            if (listCategory != null && listCategory.Any())
            {
                return ConvertCategoryToNavigation(listCategory);
            }
            return null;
        }

        public IList<NavigationItem> GetNavigationItems(Common.NavigationPosition navigationPosition)
        {
            var listNavigationItems = new List<NavigationItem>();
            var listCategory = _categoryService.GetAllCategoryByPosition(navigationPosition);
            if (listCategory != null &&listCategory.Any())
            {
                foreach (var category in listCategory)
                {
                    var navigationItem = new NavigationItem();
                    navigationItem.NavigationId = category.CategoryId;
                    navigationItem.NavigationName = category.Title;
                    var defineRouting = _routingService.GetDefineRouting(Constants.CategoryController, Constants.CategoryAction, category.CategoryId);
                    navigationItem.NavigatioUrl = defineRouting != null ? _routingService.GetUrl(Common.RoutingType.Category, category.CategoryId) : string.Empty;
                    //navigationItem.NavigationItems = GetAllNavigationChilds(category.CategoryId, navigationPosition);
                    listNavigationItems.Add(navigationItem);
                }
                return listNavigationItems;
            }
            return null;
        }

        private IList<NavigationItem> GetAllNavigationChilds(int parentId, Common.NavigationPosition navigationPosition)
        {
            var listNavigationItems = GetNavigationChilds(parentId, navigationPosition);
            if(listNavigationItems != null && listNavigationItems.Count > 0)
            {
                foreach (var navigationItem in listNavigationItems)
                {
                    navigationItem.NavigationItems = GetAllNavigationChilds(navigationItem.NavigationId, navigationPosition);
                }
            }
            return listNavigationItems;
        }

        private IList<NavigationItem> ConvertCategoryToNavigation(IEnumerable<Category> categories)
        {
            var listNavigationItems = new List<NavigationItem>();
            foreach (var category in categories)
            {
                var navigationItem = new NavigationItem();
                navigationItem.NavigationId = category.CategoryId;
                navigationItem.NavigationName = category.Title;
                var defineRouting = _routingService.GetDefineRouting(Constants.CategoryController, Constants.CategoryAction, category.CategoryId);
                navigationItem.NavigatioUrl = defineRouting != null ? _routingService.GetUrl(Common.RoutingType.Category, category.CategoryId) : string.Empty;
                listNavigationItems.Add(navigationItem);
            }
            return listNavigationItems;
        }
    }
}
