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
    public class AdvertisementRepository : GenericRepository<Advertisement>, IAdvertisementRepository
    {
        #region field and constructor
        private readonly EntityCoreContext _database;
        public AdvertisementRepository(EntityCoreContext database)
            : base(database)
        {
            _database = database;
        }

        public async Task<Advertisement> GetAdvertisementById(int id)
        {
            return await _database.Advertisement.AsNoTracking().FirstOrDefaultAsync(n => n.AdId == id);
        }
        #endregion
    }
}
