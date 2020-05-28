using DotNetCore.Data.Entity;
using DotNetCore.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Utility.ExtensionMethod;
using DotNetCore.Utility;
using static DotNetCore.Utility.Common;
using DotNetCore.Service.Interface;
using DotNetCore.Data.Interface;

namespace DotNetCore.Service
{
    public class AccountService : IAccountService
    {
        #region Field and constructor
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountRoleMapRepository _accountRoleMapRepository;
        private readonly IRoleRepository _roleRepository;

        #endregion
        public AccountService(IAccountRepository accountRepository, IAccountRoleMapRepository accountRoleMapRepository,
            IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _accountRoleMapRepository = accountRoleMapRepository;
            _roleRepository = roleRepository;
        }

        public bool Delete(int accountId)
        {
            try
            {
                var account = _accountRepository.GetAccount(accountId);
                _accountRepository.Delete(account.Result);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Account> GetAccountById(int id)
        {
            return await _accountRepository.GetAccount(id);
        }

        public Account GetAccountLogin(string username, string passwordHash)
        {
            return _accountRepository.GetAccountLogin(username, passwordHash);
        }

        public IEnumerable<Account> GetListAccounts()
        {
            return _accountRepository.GetAll().Where(n => !n.UserName.Equals("Admin", StringComparison.CurrentCultureIgnoreCase));
        }

        public bool Insert(Account account, int userId, int roles)
        {
            try
            {
                account.PasswordHash = Utility.ExtensionMethod.PasswordHash.GenerateSHA512String(Constants.DefaultPassword);
                _accountRepository.Create(account);

                var accountRoleMap = new AccountRoleMap()
                {
                    AccountId = account.AccountId,
                    RoleId = roles,
                    CreatedUserId = userId,
                    UpdatedUserId = userId
                };
                _accountRoleMapRepository.Create(accountRoleMap);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> InsertUserAccountAsync(Account account)
        {
            try
            {
                await _accountRepository.Create(account);
                var role = await _roleRepository.GetRolesByName(AccountRole.User.ToString());
                if (role != null)
                {
                    var accountRoleMap = new AccountRoleMap()
                    {
                        AccountId = account.AccountId,
                        RoleId = role.RoleId,
                    };
                    await _accountRoleMapRepository.Create(accountRoleMap);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsEmailExist(string Email)
        {
            var account = _accountRepository.GetAccountByEmail(Email);
            if (account != null)
                return true;
            return false;
        }

        public bool IsUserNameExist(string Username)
        {
            var account = _accountRepository.GetAccountByUserName(Username);
            if (account != null)
                return true;
            return false;
        }

        public bool Update(Account account)
        {
            try
            {
                _accountRepository.Update(account);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Account VerifyAccount(string username, string password)
        {
            var passwordHash = PasswordHash.GenerateSHA512String(password);
            return GetAccountLogin(username, passwordHash);
        }


    }
}
