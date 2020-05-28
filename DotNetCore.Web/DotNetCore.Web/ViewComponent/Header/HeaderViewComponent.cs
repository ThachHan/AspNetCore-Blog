using Microsoft.AspNetCore.Mvc;
using DotNetCore.Utility;
using DotNetCore.Web.Models;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web
{
    [ViewComponent(Name = "Header")]
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISystemService _systemService;
        public HeaderViewComponent(ISystemService systemService)
        {
            _systemService = systemService;
        }
        public IViewComponentResult Invoke()
        {
            var viewModel = new HeaderViewModel()
            {
                SiteLogoUrl = _systemService.GetSystemParameter(Common.SystemParameterType.SiteLogoUrl),
            };
            return View(viewModel);
        }
    }
}
