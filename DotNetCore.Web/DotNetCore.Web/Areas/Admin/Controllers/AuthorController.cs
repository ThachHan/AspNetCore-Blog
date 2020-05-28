using DotNetCore.Web.Areas.Admin.Models;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Utility;
using DotNetCore.Data.Entity;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DotNetCore.Web.Areas.Admin.Data;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator,Writer,Approver")]
    public class AuthorController : AdminBaseController
    {
        #region field and constructor
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthorController(IAuthorService authorServic, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _authorService = authorServic;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        // GET: Admin/Author
        public ActionResult Index()
        {
            var viewModel = new List<AuthorViewModel>();
            var authors = _authorService.GetListAuthor();
            if (authors != null && authors.Any())
            {
                viewModel = _mapper.Map<List<AuthorViewModel>>(authors);
            }
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> CreateAsync(AuthorViewModel viewModel, IFormFile fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fileUpload == null || fileUpload.Length == 0)
                    return Content("file not selected");

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileUpload.FileName);
                if (!System.IO.File.Exists(path))
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await fileUpload.CopyToAsync(stream);
                    }
                }
                //insert ad to database
                var author = _mapper.Map<AuthorViewModel, Author>(viewModel);
                author.ImageUrl = "/images/" + viewModel.ImageUrl;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                author.CreatedUserId = author.UpdatedUserId = user.UserId;
                author.Name = string.Format("{0} {1}", author.FirstName, author.LastName).Trim();
                if (_authorService.Insert(author, user.UserId))
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Create");

        }

        public async System.Threading.Tasks.Task<ActionResult> DetailAsync(int id = 0)
        {
            var author = await _authorService.GetAuthorById(id);
            if (author != null)
            {
                AuthorViewModel viewModel = _mapper.Map<Author, AuthorViewModel>(author);
                return View(viewModel);
            }
            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Index");
        }

        public async System.Threading.Tasks.Task<ActionResult> EditAsync(int id = 0)
        {
            var author = await _authorService.GetAuthorById(id);
            if (author != null)
            {
                AuthorViewModel viewModel = _mapper.Map<Author, AuthorViewModel>(author);
                return View("Update", viewModel);
            }
            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> EditAsync(AuthorViewModel viewModel, IFormFile fileUpload)
        {
            if (ModelState.IsValid)
            {
                //insert ad to database
                var author = _mapper.Map<AuthorViewModel, Author>(viewModel);

                if (fileUpload != null && fileUpload.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileUpload.FileName);
                    if (!System.IO.File.Exists(path))
                    {
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await fileUpload.CopyToAsync(stream);
                        }
                    }
                    author.ImageUrl = "/images/" + viewModel.ImageUrl;
                }

                var user = await _userManager.GetUserAsync(HttpContext.User);
                author.UpdatedUserId = user.UserId;
                author.Name = string.Format("{0} {1}", author.FirstName, author.LastName).Trim();
                if (_authorService.Update(author))
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Update");

        }
    }
}