using DotNetCore.Web.Areas.Admin.Models;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Utility;
using DotNetCore.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DotNetCore.Web.Areas.Admin.Data;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator,Writer,Approver")]
    public class TagController : AdminBaseController
    {
        #region field and constructor
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public TagController(ITagService tagService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _tagService = tagService;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        public ActionResult Index()
        {
            var viewModel = new List<TagViewModel>();
            var tags = _tagService.GetListTags();
            if (tags != null && tags.Any())
            {
                viewModel = _mapper.Map<List<TagViewModel>>(tags);
            }
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> CreateAsync(TagViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var tag = _mapper.Map<TagViewModel, Tag>(viewModel);
                var user = await _userManager.GetUserAsync(HttpContext.User);
                tag.CreatedUserId = tag.UpdatedUserId = user.UserId;
                if (_tagService.Insert(tag, user.UserId))
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Create");

        }

        public ActionResult Edit(int id = 0)
        {
            var tag = _tagService.GetTagById(id);
            if (tag != null)
            {
                TagViewModel viewModel = _mapper.Map<Tag, TagViewModel>(tag);
                return View("Update", viewModel);
            }
            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> EditAsync(TagViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //insert ad to database
                var tag = _mapper.Map<TagViewModel, Tag>(viewModel);
                var user = await _userManager.GetUserAsync(HttpContext.User);
                tag.UpdatedUserId = user.UserId;
                if (_tagService.Update(tag))
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Update");

        }
    }
}