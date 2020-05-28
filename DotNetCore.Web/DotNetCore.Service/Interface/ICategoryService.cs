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

namespace DotNetCore.Service.Interface
{
    public interface ICategoryService
    {
        bool Insert(Category category, int userId);
        bool Update(Category category);
        bool Delete(int categoryId);
        Task<Category> GetCategoryById(int categoryId);
        IEnumerable<Category> GetAllCategory();
        IEnumerable<Category> GetAllCategory(bool? status, int? layout);
        IEnumerable<Category> GetAllCategoryParent();
        IEnumerable<CategoryLayout> GetAllCategoryLayout();
        IEnumerable<Category> GetAllCategoryByStaus(bool isActive);
        IEnumerable<Category> GetAllCategorysForParent(int parentId);
        IEnumerable<Category> GetAllCategorysForParentAndPosition(int parentId, Common.NavigationPosition navigationPosition);
        IEnumerable<Category> GetAllCategoryByPosition(Common.NavigationPosition navigationPosition);
        IEnumerable<Category> GetCategoriesByContent(int contentId);
        bool IsExistCategoryName(string Title, int? Id);
    }
}
