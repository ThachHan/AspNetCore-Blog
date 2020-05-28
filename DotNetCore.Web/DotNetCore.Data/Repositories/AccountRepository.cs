using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.Entity;
using DotNetCore.Data.DbManager;
using Microsoft.EntityFrameworkCore;
using DotNetCore.Data.Interface;

namespace DotNetCore.Data.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        #region field and constructor
        private readonly EntityCoreContext _database;
        public AccountRepository(EntityCoreContext database)
            : base(database)
        {
            _database = database;
        }
        #endregion       

        public async Task<Account> GetAccount(int Id)
        {
            return await _database.Account.AsNoTracking().FirstOrDefaultAsync(n => n.AccountId == Id);
        }

        public async Task<Account> GetAccountAsync(string userName)
        {
            return await GetAll().FirstOrDefaultAsync(n => n.UserName.Equals(userName));
        }

        public Account GetAccountByEmail(string Email)
        {

            return GetAll().FirstOrDefault(n => n.Email == Email);
        }

        public Account GetAccountByUserName(string Username)
        {
            return GetAll().FirstOrDefault(n => n.UserName == Username);
        }

        public Account GetAccountLogin(string username, string passwordHash)
        {
            return GetAll().FirstOrDefault(n => n.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase) && n.PasswordHash.Equals(passwordHash, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
