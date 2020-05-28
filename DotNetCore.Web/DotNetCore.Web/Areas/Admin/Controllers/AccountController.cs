using DotNetCore.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using DotNetCore.Data.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using DotNetCore.Utility;
using System.Threading.Tasks;
using DotNetCore.Utility.ExtensionMethod;
using DotNetCore.Web.Areas.Admin.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using DotNetCore.Service.Interface;

namespace DotNetCore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AccountController : AdminBaseController
    {
        #region field and constructor
        private readonly IContentService _contentService;
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AccountController(IContentService contentService, IAccountService accountService,
            IRoleService roleService, IMapper mapper, IServiceProvider serviceProvider,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _contentService = contentService;
            _accountService = accountService;
            _roleService = roleService;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }
        #endregion

        public IActionResult Index()
        {
            var accounts = _accountService.GetListAccounts();
            var viewModel = new List<AccountDetailViewModel>();
            if(accounts != null && accounts.Any())
            {
                foreach (var account in accounts)
                {
                    var model = _mapper.Map<AccountDetailViewModel>(account);
                    model.Role = string.Join(",", _roleService.GetRolesForAccount(account.AccountId).Select(n => n.RoleName));
                    viewModel.Add(model);
                }
            }
            return View(viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new AccountDetailViewModel();            
            viewModel.ListRole = new SelectList(_roleService.GetListRoles(), "RoleId", "RoleName");
            return View(viewModel);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(AccountDetailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Account account = _mapper.Map<Account>(viewModel);
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var role = await _roleService.GetRoleById(viewModel.Roles);
                if (role != null)
                {
                    var result = _accountService.Insert(account, user.UserId, viewModel.Roles);
                    if (result)
                    {
                        await CreateUserAndApplyRoleAsync(account, Constants.DefaultPassword, role.RoleName);
                        return RedirectToAction("Index");
                    }
                }
            }

            //if errror, return to view
            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Create");
        }


        public async Task<ActionResult> DetailAsync(int id = 0)
        {
            var account = await _accountService.GetAccountById(id);
            if (account == null)
            {
                ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
                return RedirectToAction("Index");
            }
            AccountDetailViewModel viewAccount = _mapper.Map<Account, AccountDetailViewModel>(account);
            viewAccount.Role = string.Join(",", _roleService.GetRolesForAccount(account.AccountId).Select(n => n.RoleName));
            return View("Detail",viewAccount);
        }


        public async Task<ActionResult> Update(int id = 0)
        {
            var account = await _accountService.GetAccountById(id);
            if (account == null)
            {
                ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
                return RedirectToAction("Update");
            }
            UpdateAccountViewModel viewAccount = _mapper.Map<Account, UpdateAccountViewModel>(account);
            viewAccount.Roles = _roleService.GetRolesForAccount(account.AccountId).Select(n => n.RoleName).ToList();
            //role for combobox
            List<RoleViewModel> roles = _mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(_roleService.GetListRoles()).ToList();
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            return View(viewAccount);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(UpdateAccountViewModel model, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                Account updatedAccount = _mapper.Map<UpdateAccountViewModel, Account>(model);

                var updateResult = _accountService.Update(updatedAccount);
                if (!updateResult)
                {
                    return RedirectToAction("Update");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
                return RedirectToAction("Update");
            }

            return RedirectToAction("Index");
        }       

        [AllowAnonymous]
        public async Task<ActionResult> Login()
        {
            //return to home already login
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
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

                    // End
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return LocalRedirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", Constants.InvalidLoginErrorMsg);
                }
            }

            ModelState.AddModelError("", Constants.InvalidLoginErrorMsg);
            return RedirectToAction("Login", new { returnUrl = returnUrl });
        }

        [AllowAnonymous]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account", new { Areas = "Admin" });
        }

        public JsonResult IsEmailExist(string Email)
        {
            return Json(!_accountService.IsEmailExist(Email));
        }

        public JsonResult IsUserNameExist(string Username)
        {
            return Json(!_accountService.IsUserNameExist(Username));
        }

        public async Task<ActionResult> EditPassWordAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var account = await _accountService.GetAccountById(user.UserId);
            if (account != null)
            {
                var model = _mapper.Map<Account, EditAccountViewModel>(account);
                return View("EditPassWord",model);
            }
            ModelState.AddModelError("", Constants.UnknowErrorMessage);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> EditPasswordAsync(EditAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var oldAccount = _accountService.VerifyAccount(user.UserName, model.OldPassword);
                if (oldAccount != null)
                {
                    oldAccount.PasswordHash = PasswordHash.GenerateSHA512String(model.NewPassword);
                    oldAccount.UpdatedUserId = user.UserId;
                    _accountService.Update(oldAccount);
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
                ModelState.AddModelError("", Constants.InvalidOldPassword);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", Constants.UnknowErrorMessage);
            return RedirectToAction("Index");
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