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

    public class SubscribeRepository : GenericRepository<Subscribe>, ISubscribeRepository
    {
        #region field and constructor
        private readonly EntityCoreContext _database;
        public SubscribeRepository(EntityCoreContext database)
            : base(database)
        {
            _database = database;
        }

        public async Task<Subscribe> GetSubscribeById(int id)
        {
            return await _database.Subscribe.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        }

        #endregion
    }
}
