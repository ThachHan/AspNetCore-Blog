using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DotNetCore.Data.DbManager;
using DotNetCore.Data.Entity;
using Microsoft.EntityFrameworkCore;
using DotNetCore.Data.Interface;
namespace DotNetCore.Data.Repositories
{

    public class SystemParameterRepository : GenericRepository<SystemParameter>, ISystemParameterRepository
    {
        #region field and constructor
        private readonly EntityCoreContext _database;
        public SystemParameterRepository(EntityCoreContext database) : base(database)
        {
            _database = database;
        }

        public async Task<SystemParameter> GetSystemParameterById(int id)
        {
            return await _database.SystemParameter.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        }
        #endregion
    }
}
