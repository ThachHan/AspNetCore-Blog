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
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> GetRolesById(int id);
        Task<Role> GetRolesByName(string roleName);
    }

}
