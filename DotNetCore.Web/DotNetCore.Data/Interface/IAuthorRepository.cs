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
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<Author> GetAuthorById(int id);
    }

}
