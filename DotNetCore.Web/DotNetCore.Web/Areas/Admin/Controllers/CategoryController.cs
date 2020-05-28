using System.Collections.Generic;
using DotNetCore.Data.Entity;
using System.Linq;
using DotNetCore.Web.Areas.Admin.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using DotNetCore.Utility;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;
using DotNetCore.Web.Areas.Admin.Data;
using Microsoft.AspNetCore.Authorization;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoryController : AdminBaseController
    {
        //service
        private readonly ICategoryService _categoryService;
        private readonly IContentService _contentService;
        private readonly IRoutingService _routingService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoryController(IContentService contentService, IMapper mapper, ICategoryService categoryService,
            IRoutingService routingService, UserManager<ApplicationUser> userManager)
        {
            _categoryService = categoryService;
            _contentService = contentService;
            _mapper = mapper;
            _routingService = routingService;
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            var categories = _categoryService.GetAllCategory();
            //var listCategory = PagingList.Create(categories, _pageSize, page);
            if (categories != null && categories.Any())
            {
                var listCategory = _mapper.Map<List<Category>, List<CategoryViewModel>>(categories.ToList());

                var categoryLayouts = _categoryService.GetAllCategoryLayout();

                foreach (var category in listCategory)
                {
                    category.Status = category.IsActive ? "Visible" : "Invisible";
                    if (category.CategoryParentId.HasValue)
                    {
                        var categoryParent = _categoryService.GetCategoryById(category.CategoryParentId.Value);
                        category.CategoryParentName =
                            categoryParent != null ? categoryParent.Result.Title : string.Empty;
                    }

                    if (category.CategoryLayout.HasValue)
                    {
                        var categoryLayout =
                            categoryLayouts.FirstOrDefault(n => n.CategoryLayoutId == category.CategoryLayout);
                        category.CategoryLayoutName =
                            categoryLayout != null ? categoryLayout.CategoryLayoutName : string.Empty;
                    }
                }

                return View(listCategory);
            }
            return View();
        }

        // GET: Admin/categorys/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
                return RedirectToAction("Details");
            }
            var category = await _categoryService.GetCategoryById((int)id);
            if (category == null)
            {
                ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
                return RedirectToAction("Details");
            }
            CategoryViewModel viewModel = _mapper.Map<Category, CategoryViewModel>(category);

            return View(viewModel);
        }

        // GET: Admin/categorys/Create

        public ActionResult Create()
        {
            var categories = _categoryService.GetAllCategory();
            var categoryLayouts = _categoryService.GetAllCategoryLayout();
            var viewModel = new CategoryViewModel();
            if(categories != null && categories.Any())
            {
                viewModel.ListCategory = new SelectList(categories, "CategoryId", "Title");
            }
            if (categoryLayouts != null && categoryLayouts.Any())
            {
                viewModel.ListCategoryLayout = new SelectList(categoryLayouts, "CategoryId", "Title");
            }
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(CategoryViewModel viewModel, IFormFile fileUpload)
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
                //insert and remove output cache
                var category = _mapper.Map<CategoryViewModel, Category>(viewModel);
                category.BannerUrl = "/images/" + viewModel.BannerUrl;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                category.CreatedUserId = category.UpdatedUserId = user.UserId;
                if (_categoryService.Insert(category, user.UserId))
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Create");
        }

        // GET: Admin/Categorys/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
                return RedirectToAction("Details", new { id = id });
            }
            var category = await _categoryService.GetCategoryById((int)id);
            if (category == null)
            {
                ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
                return RedirectToAction("Details", new { id = id });
            }

            CategoryViewModel viewModel = _mapper.Map<Category, CategoryViewModel>(category);
            ViewBag.CategorysParent = _categoryService.GetAllCategory().Where(n => n.CategoryId != id);
            ViewBag.CategoryLayout = _categoryService.GetAllCategoryLayout();

            return View(viewModel);

        }

        // POST: Admin/categorys/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(CategoryViewModel viewModel, IFormFile fileUpload)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<CategoryViewModel, Category>(viewModel);
                var user = await _userManager.GetUserAsync(HttpContext.User);
                category.UpdatedUserId = user.UserId;

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
                    category.BannerUrl = "/images/" + viewModel.BannerUrl;
                }
                

                bool result = _categoryService.Update(category);

                if (result)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Edit");
        }

        // GET: Admin/categorys/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == 0)
            {
                ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
                return RedirectToAction("Delete");
            }
            var category = await _categoryService.GetCategoryById((int)id);
            if (category == null)
            {
                ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
                return RedirectToAction("Delete");
            }

            CategoryViewModel viewModel = _mapper.Map<Category, CategoryViewModel>(category);
            if (category.CategoryParentId.HasValue)
            {
                viewModel.CategoryParentName = _categoryService.GetCategoryById(category.CategoryParentId.Value).Result.Title;
            }
            return View(viewModel);
        }

        // POST: Admin/categorys/Delete/5
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (id != 0)
            {
                _categoryService.Delete(id);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
                return RedirectToAction("Delete");
            }
        }

        public JsonResult IsExistCategoryName(string Title, int? Id)
        {
            return Json(!_categoryService.IsExistCategoryName(Title, Id));
        }
    }
}