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

    public class AccountRoleMapRepository : GenericRepository<AccountRoleMap>, IAccountRoleMapRepository
    {
        #region field and constructor
        private EntityCoreContext _database;
        public AccountRoleMapRepository(EntityCoreContext database)
            : base(database)
        {
            _database = database;
        }
        #endregion

        public IEnumerable<Account> GetAccountsByRole(int roleId)
        {
            return _database.AccountRoleMap.Where(n => n.RoleId == roleId).Include(n=>n.Account).Select(n => n.Account);
        }

        public IEnumerable<Role> GetRolesForAccount(int accountId)
        {
            return _database.AccountRoleMap.Where(n => n.AccountId == accountId).Include(n => n.Role).Select(n => n.Role);
        }
        public bool DeleteAllRoleOfAccount(int accountId)
        {
            try
            {
                var accountRoleMap = GetAll().Where(n => n.AccountId == accountId).ToList();
                _database.RemoveRange(accountRoleMap);
                return true;
            }
            catch
            {
                return false;
            }
        }
    
    }
}
