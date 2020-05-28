using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.Entity;
using DotNetCore.Data.DbManager;
using Microsoft.EntityFrameworkCore;
using DotNetCore.Data.Interface;

namespace DotNetCore.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        #region field and constructor
        private readonly EntityCoreContext _database;
        public CategoryRepository(EntityCoreContext database) : base(database)
        {
            _database = database;
        }

        public IEnumerable<Category> GetAllCategoryByStaus(bool isActive)
        {
            return GetAll().Where(n => n.IsActive == isActive);
        }

        public IEnumerable<CategoryLayout> GetAllCategoryLayout()
        {
            return null;
        }

        public IEnumerable<Category> GetAllCategoryParent()
        {
            return GetAll().Where(n => n.CategoryParentId == null && n.IsActive);
        }

        public IEnumerable<Category> GetAllCategorysForParent(int parentId)
        {
            return GetAll().Where(n => n.CategoryParentId == parentId);
        }

        public async Task<Category> GetById(int id)
        {
            return await _database.Category.AsNoTracking().FirstOrDefaultAsync(n => n.CategoryId == id);
        }

        public Category GetCategoryByTitle(string title)
        {
            return GetAll().FirstOrDefault(n => n.Title.Equals(title,StringComparison.InvariantCultureIgnoreCase));
        }

        #endregion
    }
}
