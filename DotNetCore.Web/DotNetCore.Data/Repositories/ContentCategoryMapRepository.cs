using DotNetCore.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.DbManager;
using DotNetCore.Utility;
using Microsoft.EntityFrameworkCore;
using DotNetCore.Data.Interface;
namespace DotNetCore.Data.Repositories
{
    public class ContentCategoryMapRepository : GenericRepository<ContentCategoryMap>, IContentCategoryMapRepository
    {
        #region field and constructor
        private EntityCoreContext _database;
        public ContentCategoryMapRepository(EntityCoreContext database) : base(database)
        {
            _database = database;
        }

        public bool DeleteMapContentCategory(int contentId)
        {
            try
            {
                var listMap = _database.ContentCategoryMap.Where(n => n.ContentId == contentId);
                if (listMap != null && listMap.Any())
                {
                    _database.RemoveRange(listMap);
                    _database.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public IEnumerable<ContentCategoryMap> GetListMapCategoryByContent(int contentId)
        {
            return _database.ContentCategoryMap.Where(map => map.ContentId == contentId).Include(category => category.Category);
        }

        public IEnumerable<ContentCategoryMap> GetListMapContentsByCategory(int categoryId, Common.ContentStatus contentStatus)
        {
            return _database.ContentCategoryMap.Where(map=>map.CategoryId == categoryId).Include(content => content.Content).Where(content => content.Content.PostStatus == (int)contentStatus ).Include(category => category.Category);
        }

        public IEnumerable<ContentCategoryMap> GetListMapContentsByStatus(Common.ContentStatus contentStatus)
        {
            return _database.ContentCategoryMap.Include(content => content.Content).Where(content => content.Content.PostStatus == (int)contentStatus).Include(category => category.Category);
        }
        #endregion
    }
}
