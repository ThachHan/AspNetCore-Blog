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
    public interface IAuthorService
    {
        Task<Author> GetAuthorById(int id);
        IEnumerable<Author> GetListAuthor();
        bool Update(Author author);
        bool Insert(Author author, int userId);
        bool Delete(int authorId);
    }

}
