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

namespace DotNetCore.Service.Interface
{
    public interface IAccountService
    {
        Task<Account> GetAccountById(int id);
        IEnumerable<Account> GetListAccounts();
        bool Update(Account account);
        bool Insert(Account account, int userId, int roles);
        Task<bool> InsertUserAccountAsync(Account account);
        bool Delete(int accountId);
        Account VerifyAccount(string username, string password);
        Account GetAccountLogin(string username, string passwordHash);
        bool IsUserNameExist(string Username);
        bool IsEmailExist(string Email);
    }
}
