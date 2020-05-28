using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using DotNetCore.Data.Entity;
using DotNetCore.Utility;
using System.Threading.Tasks;
using DotNetCore.Web.Areas.Admin.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using DotNetCore.Web.Areas.Identity.Models;
using static DotNetCore.Utility.Common;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        #region field and constructor
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IAccountService accountService,
            IRoleService roleService, IServiceProvider serviceProvider,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _accountService = accountService;
            _roleService = roleService;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }
        #endregion
       
        [AllowAnonymous]
        public async Task<ActionResult> Login()
        {
            var viewModel = new LoginViewModel();
            //return to home already login
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            if(TempData["ErrorLogin"] != null && (bool)TempData["ErrorLogin"])
            {
                ModelState.AddModelError("", Constants.InvalidLoginErrorMsg);
            }
            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<ActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = _mapper.Map<Account>(model);
                if (account != null)
                {
                    account.PasswordHash = Utility.ExtensionMethod.PasswordHash.GenerateSHA512String(model.Password);
                    await _accountService.InsertUserAccountAsync(account);
                    await CreateUserAndApplyRoleAsync(account, model.Password, Common.AccountRole.User.ToString());
                    return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("Register");
                }
            }
            return RedirectToAction("Register");
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<ActionResult> LoginAsync(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var account = _accountService.VerifyAccount(model.UserName, model.Password);
                if (account != null)
                {
                    await CreateUserRoles(account, model.Password);

                    HttpContext.Session.SetString("UserName", string.Format("{0} {1}", account.FirstName, account.LastName));
                    // End
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        var role = _roleService.GetRolesForAccount(account.AccountId);
                        if (role.Select(n => n.RoleName).Contains(AccountRole.User.ToString()))
                        {
                            return RedirectToAction("Index", "Home", new { area = "" });
                        }
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else
                    {
                        return LocalRedirect(returnUrl);
                    }
                }
            }

            TempData["ErrorLogin"] = true; 
            return RedirectToAction("Login", new { returnUrl = returnUrl });
        }

        [AllowAnonymous]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }
       
        private async Task CreateUserRoles(Account account, string password)
        {
            IdentityResult roleResult;

            var roles = _roleService.GetRolesForAccount(account.AccountId);
            if (roles != null && roles.Any())
            {
                foreach (var role in roles)
                {
                    var roleCheck = await _roleManager.RoleExistsAsync(role.RoleName);
                    if (!roleCheck)
                    {
                        //create the roles and seed them to the database    
                        roleResult = await _roleManager.CreateAsync(new IdentityRole(role.RoleName));
                    }
                    ApplicationUser user = await _userManager.FindByNameAsync(account.UserName);
                    if (user == null)
                    {
                        user = new ApplicationUser()
                        {
                            UserId = account.AccountId,
                            UserName = account.UserName,
                            Email = account.Email,
                            SecurityStamp = Guid.NewGuid().ToString()
                        };
                        var createPowerUser = await _userManager.CreateAsync(user, password);
                        if (createPowerUser.Succeeded)
                        {
                            // here we assign the new user the "Admin" role 
                            await _userManager.AddToRoleAsync(user, role.RoleName);
                        }
                        else
                        {
                            var errors = createPowerUser.Errors;
                            var message = string.Join(", ", errors);
                        }
                    }
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, role.RoleName),
                        new Claim(ClaimTypes.Email, user.Email)
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
                    };

                    await _signInManager.SignInAsync(user, authProperties);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                }
            }
        }

        public JsonResult IsEmailExist(string Email)
        {
            return Json(!_accountService.IsEmailExist(Email));
        }

        public JsonResult IsUserNameExist(string Username)
        {
            return Json(!_accountService.IsUserNameExist(Username));
        }
        public async Task CreateUserAndApplyRoleAsync(Account account, string password, string role)
        {
            var roleCheck = await _roleManager.RoleExistsAsync(role);
            if (!roleCheck)
            {
                //create the roles and seed them to the database    
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
            var user = new ApplicationUser()
            {
                UserId = account.AccountId,
                UserName = account.UserName,
                Email = account.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var createPowerUser = await _userManager.CreateAsync(user, password);
            if (createPowerUser.Succeeded)
            {
                // here we assign the new user the "Admin" role 
                await _userManager.AddToRoleAsync(user, role);
            }
            else
            {
                var errors = createPowerUser.Errors;
                var message = string.Join(", ", errors);
            }
        }
    }

}