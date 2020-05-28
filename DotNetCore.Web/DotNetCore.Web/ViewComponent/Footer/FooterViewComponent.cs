using Microsoft.AspNetCore.Mvc;
using DotNetCore.Utility;
using DotNetCore.Web.Models;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web
{
    [ViewComponent(Name = "Footer")]
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISystemService _systemService;
        public FooterViewComponent(ISystemService systemService)
        {
            _systemService = systemService;
        }
        public IViewComponentResult Invoke()
        {
            var viewModel = new SummaryWebsiteViewModel()
            {
                SiteLogoUrl = _systemService.GetSystemParameter(Common.SystemParameterType.SiteLogoFooterUrl),
                SiteDescription = _systemService.GetSystemParameter(Common.SystemParameterType.SiteDescription),
                CopyRight = _systemService.GetSystemParameter(Common.SystemParameterType.CopyRight)
            };
            return View(viewModel);
        }
    }
}
