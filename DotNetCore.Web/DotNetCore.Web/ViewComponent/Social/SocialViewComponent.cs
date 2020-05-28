using Microsoft.AspNetCore.Mvc;
using DotNetCore.Utility;
using DotNetCore.Web.Models;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web
{
    [ViewComponent(Name = "Social")]
    public class SocialViewComponent : ViewComponent
    {
        private readonly ISystemService _systemService;
        public SocialViewComponent(ISystemService systemService)
        {
            _systemService = systemService;
        }

        public IViewComponentResult Invoke(Common.SocialPosition socialPosition)
        {
            var viewModel = new SocialViewModel()
            {
                FacebookLink = _systemService.GetSystemParameter(Common.SystemParameterType.FacebookLink),
                FacebookFollowers = _systemService.GetSystemParameter(Common.SystemParameterType.FacebookFollowers),
                TwitterLink = _systemService.GetSystemParameter(Common.SystemParameterType.TwitterLink),
                TwitterFollowers = _systemService.GetSystemParameter(Common.SystemParameterType.TwitterFollowers),
                GoogleLink = _systemService.GetSystemParameter(Common.SystemParameterType.GoogleLink),
                GoogleFollowers = _systemService.GetSystemParameter(Common.SystemParameterType.GoogleFollowers),
                InstagramLink = _systemService.GetSystemParameter(Common.SystemParameterType.InstagramLink),
                InstagramFollowers = _systemService.GetSystemParameter(Common.SystemParameterType.InstagramFollowers),
            };
            var viewName = socialPosition == Common.SocialPosition.Body ? "SocialBody" : (socialPosition == Common.SocialPosition.Footer ? "SocialFooter" : "SocialNavigation");
            return View(viewName, viewModel);
        }
    }
}
