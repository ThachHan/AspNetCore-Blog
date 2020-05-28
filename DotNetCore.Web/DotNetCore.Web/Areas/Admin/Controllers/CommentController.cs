using DotNetCore.Web.Areas.Admin.Models;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DotNetCore.Web.Areas.Admin.Data;
using System.Threading.Tasks;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CommentController : AdminBaseController
    {
        #region field and constructor
        private readonly ICommentService _commentService ;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(ICommentService commentService, IMapper mapper, UserManager<ApplicationUser> userManager,
            IAccountService accountService)
        {
            _commentService = commentService;
            _mapper = mapper;
            _userManager = userManager;
            _accountService = accountService;
        }
        #endregion

        // GET: Admin/Author
        public async System.Threading.Tasks.Task<ActionResult> LoadCommentAsync(int id)
        {
            var viewModel = new List<CommentViewModel>();
            var comments = await _commentService.GetListCommentByContent(id);
            if (comments != null && comments.Any())
            {
                foreach (var comment in comments)
                {
                    var model = _mapper.Map<CommentViewModel>(comment);
                    model.Status = model.IsActive ? "Active" : "InActive";
                    model.TypeComment = "Main Comment";
                    var account = await _accountService.GetAccountById(comment.UserId);
                    if(account != null)
                    {
                        model.UserViewModel = _mapper.Map<AccountViewModel>(account);
                    }
                    viewModel.Add(model);
                    var childComments = await _commentService.GetListCommentByParent(comment.Id);
                    if (childComments != null && childComments.Any())
                    {
                        foreach (var child in childComments)
                        {
                            var childComment = _mapper.Map<CommentViewModel>(child);
                            childComment.Status = childComment.IsActive ? "Active" : "InActive";
                            childComment.TypeComment = "Reply Comment";
                            var childAccount = await _accountService.GetAccountById(child.UserId);
                            if (childAccount != null)
                            {
                                childComment.UserViewModel = _mapper.Map<AccountViewModel>(childAccount);
                            }
                            viewModel.Add(childComment);
                        }
                    }
                    
                }
            }
            return View("LoadComment",viewModel);
        }
        
        [HttpPost]
        public async Task<ActionResult> RejectAsync(string id)
        {
            if (ModelState.IsValid)
            {
                int result = 0;
                if (int.TryParse(id, out result))
                {
                    var comment = await _commentService.GetCommentById(result);
                    var user = await _userManager.GetUserAsync(HttpContext.User);

                    comment.UpdatedUserId = user.UserId;
                    comment.IsActive = false;
                    if (_commentService.Update(comment))
                    {
                        return Json(new { result = true, contentId = comment.ContentId });
                    }
                }
            }

            return Json(new { result = false });
        }

        [HttpPost]
        public async Task<ActionResult> ApproveAsync(string id)
        {
            if (ModelState.IsValid)
            {
                int result = 0;
                if (int.TryParse(id, out result))
                {
                    var comment = await _commentService.GetCommentById(result);
                    var user = await _userManager.GetUserAsync(HttpContext.User);

                    comment.UpdatedUserId = user.UserId;
                    comment.IsActive = true;
                    if (_commentService.Update(comment))
                    {
                        return Json(new { result = true, contentId = comment.ContentId });
                    }
                }
            }

            return Json(new { result = false });
        }
    }
}