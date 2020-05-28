using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.Entity;
using DotNetCore.Data.DbManager;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.Data.Interface
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IEnumerable<Category> GetAllCategoryParent();
        IEnumerable<CategoryLayout> GetAllCategoryLayout();
        IEnumerable<Category> GetAllCategoryByStaus(bool isActive);
        IEnumerable<Category> GetAllCategorysForParent(int parentId);
        Category GetCategoryByTitle(string title);
        Task<Category> GetById(int id);
    }


}
