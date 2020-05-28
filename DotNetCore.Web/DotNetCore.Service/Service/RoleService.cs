using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.Data.Entity;
using DotNetCore.Data.Repositories;
using System.IO;
using DotNetCore.Utility;
using DotNetCore.Service.Interface;
using DotNetCore.Data.Interface;
namespace DotNetCore.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _rolesRepository;
        private readonly IAccountRoleMapRepository _accountRoleMapRepository;
        public RoleService(IRoleRepository rolesRepository, IAccountRoleMapRepository accountRoleMapRepository)
        {
            _rolesRepository = rolesRepository;
            _accountRoleMapRepository = accountRoleMapRepository;
        }

        public bool Delete(int roleId)
        {
            try
            {
                var role = GetRoleById(roleId);
                _rolesRepository.Delete(role.Result);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _rolesRepository.GetRolesById(id);
        }

        public IEnumerable<Role> GetListRoles()
        {
            return _rolesRepository.GetAll().Where(n=>!n.RoleName.Equals(Common.AccountRole.User.ToString()));
        }
        
        public bool Insert(Role role)
        {
            try
            {
                _rolesRepository.Create(role);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Role role)
        {
            try
            {
                _rolesRepository.Update(role);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertMap(int accountId, int[] roles)
        {
            try
            {
                DeleteMap(accountId);
                if(roles != null && roles.Any())
                {
                    foreach (var role in roles)
                    {
                        var roleMap = new AccountRoleMap()
                        {
                            AccountId = accountId,
                            RoleId = role,
                        };
                        _accountRoleMapRepository.Create(roleMap);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteMap(int accountId)
        {
            try
            {
                _accountRoleMapRepository.DeleteAllRoleOfAccount(accountId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Role> GetRolesForAccount(int accountId)
        {
            return _accountRoleMapRepository.GetRolesForAccount(accountId);
        }
    }
}
