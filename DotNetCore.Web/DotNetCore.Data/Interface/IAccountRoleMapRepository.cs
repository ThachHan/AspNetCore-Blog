using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.DbManager;
using DotNetCore.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.Data.Interface
{
    public interface IAccountRoleMapRepository : IGenericRepository<AccountRoleMap>
    {
        IEnumerable<Account> GetAccountsByRole(int roleId);
        IEnumerable<Role> GetRolesForAccount(int accountId);
        bool DeleteAllRoleOfAccount(int accountId);
    }   
}
