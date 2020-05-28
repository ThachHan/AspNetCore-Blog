using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DotNetCore.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DotNetCore.Data.Entity;
using System.Threading.Tasks;
using DotNetCore.Utility;
using Microsoft.AspNetCore.Identity;
using DotNetCore.Web.Areas.Admin.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web.Areas.Admin.Controllers
{
    public class ContentController : AdminBaseController
    {
        private readonly IContentService _contentService;
        private readonly ICategoryService _categoryService;
        private readonly IRoutingService _routingService;
        private readonly IAccountService _accountService;
        private readonly IAuthorService _authorService;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public ContentController(IContentService contentService, ICategoryService categoryService,
            IMapper mapper, UserManager<ApplicationUser> userManager, IAuthorService authorService,
            IRoutingService routingService, IAccountService accountService, ITagService tagService)
        {
            _contentService = contentService;
            _categoryService = categoryService;
            _routingService = routingService;
            _accountService = accountService;
            _mapper = mapper;
            _userManager = userManager;
            _authorService = authorService;
            _tagService = tagService;
        }

        [Authorize(Roles = "Administrator,Writer,Approver")]
        public ActionResult WritedContents()
        {
            var contents = _contentService.GetListContentsByStatus(Common.ContentStatus.Writed);
            if (contents != null && contents.Any())
            {
                var viewModel = _mapper.Map<List<Content>, List<ContentViewModel>>(contents.ToList());
                return View(viewModel);
            }
            return View();
        }
        [Authorize(Roles = "Administrator,Approver")]
        public ActionResult ApprovedContents()
        {
            var contents = _contentService.GetListContentsByStatus(Common.ContentStatus.Approved);
            if (contents != null)
            {
                var viewModel = _mapper.Map<List<Content>, List<ContentViewModel>>(contents.ToList());
                return View(viewModel);
            }
            return View();
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult PublishedContents()
        {
            var contents = _contentService.GetListContentsByStatus(Common.ContentStatus.Published);
            if (contents != null)
            {
                var viewModel = _mapper.Map<List<Content>, List<ContentViewModel>>(contents.ToList());
                return View(viewModel);
            }
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Administrator,Approver")]
        public async Task<ActionResult> ApproveAsync(string id)
        {
            if (ModelState.IsValid)
            {
                int result = 0;
                if (int.TryParse(id, out result))
                {
                    var content = await _contentService.GetContentById(result);
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    content.PostStatus = (int)Common.ContentStatus.Approved;
                    content.UpdatedUserId = user.UserId;

                    if (_contentService.Update(content))
                    {
                        return RedirectToAction("ApprovedContents");
                    }
                }
            }

            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("WritedContents");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Approver")]
        public async Task<ActionResult> RejectAsync(string id)
        {
            if (ModelState.IsValid)
            {
                int result = 0;
                if (int.TryParse(id, out result))
                {
                    var content = await _contentService.GetContentById(result);
                    var user = await _userManager.GetUserAsync(HttpContext.User);

                    content.PostStatus = (int)Common.ContentStatus.Writed;
                    content.UpdatedUserId = user.UserId;

                    if (_contentService.Update(content))
                    {
                        return RedirectToAction("WritedContents");
                    }
                }
            }

            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("WritedContents");
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Publish(int? id)
        {
            if (id.HasValue)
            {
                var content = _contentService.GetContentById(id.Value);
                if (content.Result != null && content.Result.PostStatus != (int)Common.ContentStatus.Published)
                {
                    var viewModel = _mapper.Map<Content, PublishingContentViewModel>(content.Result);
                    viewModel.ListCategory = new MultiSelectList(_categoryService.GetAllCategory(), "CategoryId", "Title");
                    return View(viewModel);
                }
            }
            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("ApprovedContents");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Approver")]
        public async Task<ActionResult> PublishAsync(PublishingContentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var content = await _contentService.GetContentById(viewModel.ContentId);
                var user = await _userManager.GetUserAsync(HttpContext.User);
                content.PostStatus = (int)Common.ContentStatus.Published;
                content.UpdatedUserId = user.UserId;

                if (_contentService.Update(content) && _contentService.InsertMapContentCategories(content.ContentId, viewModel.Categories))
                {
                    return RedirectToAction("PublishedContents");
                }
            }

            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("ApprovedContents");
        }

        //Show detail content when user click button detail on screen
        [Authorize(Roles = "Administrator,Writer,Approver")]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                var content = _contentService.GetContentById(id.Value);
                if (content.Result != null)
                {
                    var viewModel = _mapper.Map<Content, ContentViewModel>(content.Result);
                    viewModel.NameStatus = ((Common.ContentStatus)viewModel.PostStatus).ToString();
                    var author = _authorService.GetAuthorById(viewModel.AuthorId);
                    viewModel.AuthorName = author != null ? author.Result.Name : string.Empty;
                    var tags = _tagService.GetTagByContent(content.Result.ContentId);
                    if(tags != null && tags.Any())
                    {
                        viewModel.Tags = string.Join(",", tags.Select(n => n.TagName));
                    }
                    return View(viewModel);
                }
            }
            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Details");

        }

        [Authorize(Roles = "Administrator,Writer,Approver")]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var content = _contentService.GetContentById(id.Value);
                if (content.Result != null)
                {
                    var viewModel = _mapper.Map<Content, ContentViewModel>(content.Result);
                    viewModel.NameStatus = ((Common.ContentStatus)viewModel.PostStatus).ToString();
                    var authors = _authorService.GetListAuthor();
                    var tags = _tagService.GetListTags();
                    if (authors != null && authors.Any())
                    {
                        viewModel.ListAuthor = new SelectList(authors, "AuthorId", "Name");
                    }
                    if (tags != null && tags.Any())
                    {
                        viewModel.ListTag = new MultiSelectList(tags, "TagId", "TagName");
                    }
                    var tagsOfContent = _tagService.GetTagByContent(id.Value);
                    if (tagsOfContent != null && tagsOfContent.Any())
                    {
                        viewModel.Tag = tagsOfContent.Select(n => n.TagId).ToArray();
                    }
                    return View(viewModel);
                }
            }
            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Edit");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Writer,Approver")]
        public async Task<ActionResult> EditAsync(ContentViewModel viewModel, IFormFile fileUpload)
        {
            if (ModelState.IsValid)
            {
                Content content = _mapper.Map<ContentViewModel, Content>(viewModel);

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
                    content.BannerUrl = "/images/" + viewModel.BannerUrl;
                }
                var user = await _userManager.GetUserAsync(HttpContext.User);
                content.PostStatus = (int)Common.ContentStatus.Writed;
                content.UpdatedUserId = user.UserId;
                bool result = _contentService.Update(content, viewModel.Tag);

                if (result)
                {
                    return RedirectToAction("WritedContents");
                }
            }

            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Edit");
        }

        [Authorize(Roles = "Administrator,Writer,Approver")]
        public ActionResult Create()
        {
            var authors = _authorService.GetListAuthor();
            var tags = _tagService.GetListTags();
            var viewModel = new ContentViewModel();
            if (authors != null && authors.Any())
            {
                viewModel.ListAuthor = new SelectList(authors, "AuthorId", "Name");
            }
            if (tags != null && tags.Any())
            {
                viewModel.ListTag = new MultiSelectList(tags, "TagId", "TagName");
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Writer,Approver")]
        public async Task<ActionResult> CreateAsync(ContentViewModel viewModel, IFormFile fileUpload)
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
                var content = _mapper.Map<ContentViewModel, Content>(viewModel);
                var user = await _userManager.GetUserAsync(HttpContext.User);
                content.BannerUrl = "/images/" + viewModel.BannerUrl;
                content.CreatedUserId = content.UpdatedUserId = user.UserId;
                content.PostStatus = (int)Common.ContentStatus.Writed;
                if (_contentService.Insert(content, user.UserId, viewModel.Tag))
                {
                    return RedirectToAction("WritedContents");
                }
            }

            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Create");
        }
        [Authorize(Roles = "Administrator,Writer,Approver")]
        public JsonResult IsExistContentName(string Title, int? Id)
        {
            return Json(!_contentService.IsExistContentName(Title, Id));
        }

    }
}