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
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        #region field and constructor
        private readonly EntityCoreContext _database;
        public RoleRepository(EntityCoreContext database)
            : base(database)
        {
            _database = database;
        }

        public async Task<Role> GetRolesById(int id)
        {
            return await _database.Roles.AsNoTracking().FirstOrDefaultAsync(n => n.RoleId == id);
        }

        public async Task<Role> GetRolesByName(string roleName)
        {
            return await _database.Roles.AsNoTracking().FirstOrDefaultAsync(n => n.RoleName.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
        }


        #endregion
    }
}
