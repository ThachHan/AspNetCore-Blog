using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.DbManager;
using DotNetCore.Data.Entity;
using Microsoft.EntityFrameworkCore;
using DotNetCore.Data.Interface;
namespace DotNetCore.Data.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        #region field and constructor
        private readonly EntityCoreContext _database;
        public TagRepository(EntityCoreContext database)
            : base(database)
        {
            _database = database;
        }

        public Tag GetTagById(int id)
        {
            return _database.Tag.AsNoTracking().FirstOrDefault(n => n.TagId == id);
        }

        public bool RemoveListTag(IEnumerable<Tag> tags)
        {
            try
            {
                _database.Tag.RemoveRange(tags);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
