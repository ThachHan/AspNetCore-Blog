using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Utility;
using DotNetCore.Web.Models;
using AutoMapper;
using DotNetCore.Web.Areas.Admin.Data;
using Microsoft.AspNetCore.Identity;
using static DotNetCore.Utility.Common;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web
{
    [ViewComponent(Name = "User")]
    public class UserViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        public UserViewComponent(UserManager<ApplicationUser> userManager, IMapper mapper, IAccountService accountService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _accountService = accountService;
        }
        public async Task<IViewComponentResult> InvokeAsync(Common.UserPosition userPosition)
        {
            var viewName = userPosition == Common.UserPosition.AsideNavigation ? "UserAside" : "UserMain";
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains(AccountRole.User.ToString()))
                {
                    var account = await _accountService.GetAccountById(user.UserId);
                    if (account != null)
                    {
                        var viewModel = _mapper.Map<AccountViewModel>(account);
                        viewModel.FullName = string.Format("{0} {1}", viewModel.FirstName, viewModel.LastName);
                        return View(viewName, viewModel);
                    }
                }
            }
            return View(viewName,new AccountViewModel());
        }
    }
}
