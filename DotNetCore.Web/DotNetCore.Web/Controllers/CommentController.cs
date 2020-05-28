using AutoMapper;
using DotNetCore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DotNetCore.Web.Areas.Admin.Data;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web.Controllers
{
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public CommentController(IMapper mapper, ICommentService commentService, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpPost, Authorize(Roles ="User")]
        public async System.Threading.Tasks.Task<ActionResult> SaveCommentAsync(PostCommentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var comment = _mapper.Map<Comment>(viewModel);
                var user = await _userManager.GetUserAsync(HttpContext.User);
                comment.UserId = user.UserId;
                comment.IsActive = true;
                if(_commentService.Insert(comment))
                {
                    return Json(true);
                }
            }
            return Json(false);
        }
    }
}