using DotNetCore.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.DbManager;
using Microsoft.EntityFrameworkCore;
using DotNetCore.Utility;
using DotNetCore.Data.Interface;
namespace DotNetCore.Data.Repositories
{
    public class ContentRepository : GenericRepository<Content>, IContentRepository
    {
        #region field and constructor
        private EntityCoreContext _database;
        public ContentRepository(EntityCoreContext database) : base(database)
        {
            _database = database;
        }

        public async Task<Content> GetContentById(int Id)
        {
            return await _database.Content.AsNoTracking().FirstOrDefaultAsync(n => n.ContentId == Id);
        }

        public Content GetContentByTitle(string title)
        {
            return _database.Content.FirstOrDefault(n => n.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<Content> GetListContentsByStatus(Common.ContentStatus contentStatus)
        {
            return _database.Content.Where(n => n.PostStatus == (int)contentStatus);
        }

        public async Task<IEnumerable<Content>> SearchContent(string key)
        {
            return await _database.Content.Where(n => n.Title.Contains(key, StringComparison.CurrentCultureIgnoreCase)).ToArrayAsync();
        }


        #endregion
    }
}
