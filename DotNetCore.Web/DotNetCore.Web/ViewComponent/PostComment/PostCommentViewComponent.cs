using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Web.Models;
using Microsoft.AspNetCore.Identity;
using DotNetCore.Web.Areas.Admin.Data;
using static DotNetCore.Utility.Common;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web
{
    [ViewComponent(Name = "PostComment")]
    public class PostCommentViewComponent : ViewComponent
    {
        private readonly IContentService _contentService;
        private readonly UserManager<ApplicationUser> _userManager;
        public PostCommentViewComponent(IContentService contentService, UserManager<ApplicationUser> userManager)
        {
            _contentService = contentService;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? contentId)
        {
            var viewModel = new PostCommentViewModel();
            if (contentId.HasValue)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var content = await _contentService.GetContentById(contentId.Value);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (content != null && roles != null && roles.Contains(AccountRole.User.ToString()) && content.IsAllowComment)
                    {
                        viewModel.IsAllowComment = true;
                    }
                }
                viewModel.ContentId = contentId.Value;
            }
            return View(viewModel);
        }
    }
}
