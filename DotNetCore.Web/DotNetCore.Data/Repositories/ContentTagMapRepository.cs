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
    public class ContentTagMapRepository : GenericRepository<ContentTagMap>, IContentTagMapRepository
    {
        #region field and constructor
        private EntityCoreContext _database;
        public ContentTagMapRepository(EntityCoreContext database) : base(database)
        {
            _database = database;
        }

        public bool DeleteMap(int contentId)
        {
            try
            {
                var contentTagMaps = GetListMapTagByContent(contentId);
                _database.RemoveRange(contentTagMaps);
                _database.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ContentTagMap GetContentTagMap(int contentId, int tagId)
        {
            return _database.ContentTagMap.AsNoTracking().FirstOrDefault(n => n.ContentId == contentId && n.TagId == tagId);
        }

        public IEnumerable<ContentTagMap> GetListMapContentsByTag(int tagId, Common.ContentStatus contentStatus)
        {
            return _database.ContentTagMap.Where(map => map.TagId == tagId).Where(content => content.Content.PostStatus == (int)contentStatus).Include(content => content.Content).Include(tag => tag.Tag);
        }

        public IEnumerable<ContentTagMap> GetListMapTagByContent(int contentId)
        {
            return _database.ContentTagMap.Where(map => map.ContentId == contentId).Include(content => content.Content).Include(tag => tag.Tag);
        }
        #endregion
    }
}
