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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IRoutingService _routingService;
        public AuthorService(IAuthorRepository authorRepository, IRoutingService routingService)
        {
            _authorRepository = authorRepository;
            _routingService = routingService;
        }

        public bool Delete(int authorId)
        {
            try
            {
                var author = GetAuthorById(authorId);
                _authorRepository.Delete(author.Result);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _authorRepository.GetAuthorById(id);
        }

        public IEnumerable<Author> GetListAuthor()
        {
            return _authorRepository.GetAll();
        }


        public bool Insert(Author author, int userId)
        {
            try
            {
                _authorRepository.Create(author);
                _routingService.Insert(Common.RoutingType.Author, userId, author : author);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Author author)
        {
            try
            {
                _authorRepository.Update(author);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
