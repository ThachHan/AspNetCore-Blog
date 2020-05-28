using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using DotNetCore.Web.Areas.Admin.Data;
using static DotNetCore.Utility.Common;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web
{
    [ViewComponent(Name = "Comment")]
    public class CommentViewComponent : ViewComponent
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly IContentService _contentService;
        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;
        public CommentViewComponent(ICommentService commentService, IMapper mapper, IContentService contentService, UserManager<ApplicationUser> userManager,
            IAccountService accountService)
        {
            _commentService = commentService;
            _contentService = contentService;
            _userManager = userManager;
            _accountService = accountService;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? contentId)
        {
            var viewModel = new CommentViewModel();
            if (contentId.HasValue)
            {
                viewModel.ContentId = contentId.Value;
                viewModel.TotalComment = await _commentService.GetTotalCommentByContentAsync(contentId.Value);
                var content = await _contentService.GetContentById(contentId.Value);
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if(user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (content != null && roles != null && roles.Contains(AccountRole.User.ToString()) && content.IsAllowComment)
                    {
                        viewModel.IsAllowComment = true;
                    }
                }
                
                var comments = await _commentService.GetListShowedCommentByContent(contentId.Value);
                if (comments != null && comments.Any())
                {
                    viewModel.UserComments = _mapper.Map<List<UserCommentViewModel>>(comments.ToList());
                    foreach (var userComment in viewModel.UserComments)
                    {
                        var account = await _accountService.GetAccountById(userComment.UserId);
                        if(account != null)
                        {
                            userComment.UserComment = _mapper.Map<AccountViewModel>(account);
                            userComment.UserComment.FullName = string.Format("{0} {1}", account.FirstName, account.LastName);
                        }
                        var childComment = await _commentService.GetListShowedCommentByParent(userComment.Id);
                        if(childComment != null && childComment.Any())
                        {
                            userComment.replyViewModels = _mapper.Map<List<UserCommentViewModel>>(childComment.ToList());
                            foreach (var replyComment in userComment.replyViewModels)
                            {
                                var accountReply = await _accountService.GetAccountById(replyComment.UserId);
                                if (accountReply != null)
                                {
                                    replyComment.UserComment = _mapper.Map<AccountViewModel>(accountReply);
                                    replyComment.UserComment.FullName = string.Format("{0} {1}", accountReply.FirstName, accountReply.LastName);
                                }
                            }
                        }
                    }
                }
            }
            return View(viewModel);
        }
    }
}
