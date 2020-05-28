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

    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        #region field and constructor
        private readonly EntityCoreContext _database;
        public AuthorRepository(EntityCoreContext database)
            : base(database)
        {
            _database = database;
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _database.Author.AsNoTracking().FirstOrDefaultAsync(n => n.AuthorId == id);
        }
        #endregion


    }
}
