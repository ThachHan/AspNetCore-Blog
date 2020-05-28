using AutoMapper;
using DotNetCore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IContentService _contentService;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        public AuthorController(IMapper mapper, ICategoryService categoryService, IContentService contentService
            , IAuthorService authorService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _contentService = contentService;
            _authorService = authorService;
        }

        // GET: Category
        public async System.Threading.Tasks.Task<ActionResult> LoadAuthorAsync(int? id)
        {
            if(id.HasValue)
            {
                var viewModel = new AuthorViewModel();
                var author = await _authorService.GetAuthorById(id.Value);
                if(author != null)
                {
                    viewModel = _mapper.Map<AuthorViewModel>(author);
                    return View("LoadAuthor",viewModel);
                }
            }
            return null;
        }
    }
}