using AutoMapper;
using DotNetCore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web.Controllers
{
    public class TagController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IContentService _contentService;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        public TagController(IMapper mapper, ICategoryService categoryService, IContentService contentService
            , IAuthorService authorService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _contentService = contentService;
            _authorService = authorService;
        }

        // GET: Category
        public ActionResult LoadTag(int? id)
        {
            if (id.HasValue)
            {
                var viewModel = new TagViewModel()
                {
                    TagId = id.Value
                };

                return View(viewModel);
            }
            return null;
        }
    }
}