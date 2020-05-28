using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.Entity;
using DotNetCore.Data.DbManager;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.Data.Interface
{
    public interface IAccountRepository : IGenericRepository<Account>
    {

        Task<Account> GetAccount(int Id);
        Task<Account> GetAccountAsync(string userName);
        Account GetAccountLogin(string username, string passwordHash);
        Account GetAccountByUserName(string Username);
        Account GetAccountByEmail(string Email);
    }  
}
