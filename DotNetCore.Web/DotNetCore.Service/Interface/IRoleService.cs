using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.Data.Entity;
using DotNetCore.Data.Repositories;
using System.IO;
using DotNetCore.Utility;

namespace DotNetCore.Service.Interface
{
    public interface IRoleService
    {
        Task<Role> GetRoleById(int id);
        IEnumerable<Role> GetListRoles();
        bool Update(Role role);
        bool Insert(Role role);
        bool Delete(int roleId);
        bool InsertMap(int accountId, int[] roles);
        bool DeleteMap(int accountId);
        IEnumerable<Role> GetRolesForAccount(int accountId);
    }
}
