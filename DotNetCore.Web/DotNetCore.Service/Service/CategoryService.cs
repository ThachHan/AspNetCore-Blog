using DotNetCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DotNetCore.Data.Entity;
using DotNetCore.Data.Repositories;
using DotNetCore.Data.Models;
using DotNetCore.Utility;
using DotNetCore.Service.Interface;
using DotNetCore.Data.Interface;
namespace DotNetCore.Service
{

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IContentCategoryMapRepository _contentCategoryMapRepository;
        private readonly IRoutingService _routingService;
        public CategoryService(ICategoryRepository categoryRepository, IRoutingService routingService,
            IContentCategoryMapRepository contentCategoryMapRepository)
        {
            _categoryRepository = categoryRepository;
            _routingService = routingService;
            _contentCategoryMapRepository = contentCategoryMapRepository;
        }

        public bool Delete(int categoryId)
        {
            try
            {
                var category = GetCategoryById(categoryId);
                _categoryRepository.Delete(category.Result);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Category> GetAllCategory()
        {
            return _categoryRepository.GetAll();
        }

        public IEnumerable<Category> GetAllCategory(bool? status, int? layout)
        {
            var categories = GetAllCategory();
            if (status.HasValue)
            {
                categories = categories.Where(n => n.IsActive == status.Value);
            }
            if(layout.HasValue)
            {
                categories = categories.Where(n => n.CategoryLayoutId == layout.Value);
            }
            return categories;
        }

        public IEnumerable<Category> GetAllCategoryByPosition(Common.NavigationPosition navigationPosition)
        {
            if (navigationPosition == Common.NavigationPosition.Main || navigationPosition == Common.NavigationPosition.Aside)
            {
                return GetAllCategory().Where(n => n.IsShowTopMenu && n.IsActive);
            }
            else if (navigationPosition == Common.NavigationPosition.Footer)
            {
                return GetAllCategory().Where(n => n.IsShowBotMenu && n.IsActive);
            }
            return null;
        }

        public IEnumerable<Category> GetAllCategoryByStaus(bool isActive)
        {
            return _categoryRepository.GetAllCategoryByStaus(isActive);
        }

        public IEnumerable<CategoryLayout> GetAllCategoryLayout()
        {
            return _categoryRepository.GetAllCategoryLayout();
        }

        public IEnumerable<Category> GetAllCategoryParent()
        {
            return _categoryRepository.GetAllCategoryParent();
        }

        public IEnumerable<Category> GetAllCategorysForParent(int parentId)
        {
            return _categoryRepository.GetAllCategorysForParent(parentId);
        }

        public IEnumerable<Category> GetAllCategorysForParentAndPosition(int parentId, Common.NavigationPosition navigationPosition)
        {
            if (navigationPosition == Common.NavigationPosition.Main || navigationPosition == Common.NavigationPosition.Aside)
            {
                return GetAllCategorysForParent(parentId).Where(n => n.IsActive && n.IsShowTopMenu);
            }
            else if (navigationPosition == Common.NavigationPosition.Footer)
            {
                return GetAllCategorysForParent(parentId).Where(n => n.IsActive && n.IsShowBotMenu);
            }
            return null;
        }

        public IEnumerable<Category> GetCategoriesByContent(int contentId)
        {
            return _contentCategoryMapRepository.GetListMapCategoryByContent(contentId).Select(n=>n.Category);
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await _categoryRepository.GetById(categoryId);
        }

        public bool Insert(Category category, int userId)
        {
            try
            {
                _categoryRepository.Create(category);
                _routingService.Insert(Common.RoutingType.Category,userId, category: category);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsExistCategoryName(string Title, int? Id)
        {
            Title = Title.Trim();

            var isExist = _categoryRepository.GetCategoryByTitle(Title);
            if (isExist != null)
            {
                if (Id.HasValue && isExist.CategoryId == Id.Value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool Update(Category category)
        {
            try
            {
                _categoryRepository.Update(category);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
