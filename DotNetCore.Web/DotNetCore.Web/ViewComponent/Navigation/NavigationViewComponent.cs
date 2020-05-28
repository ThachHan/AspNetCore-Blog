using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Utility;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web
{
    [ViewComponent(Name = "Navigation")]
    public class NavigationViewComponent : ViewComponent
    {
        private readonly INavigationService _navigationService;
        public NavigationViewComponent(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public IViewComponentResult Invoke(Common.NavigationPosition navigationPosition)
        {
            var viewName = navigationPosition == Common.NavigationPosition.Main ? "MainNavigation" : (navigationPosition == Common.NavigationPosition.Aside ? "AsideNavigation" : "FooterNavigation");
            var listNavigation = _navigationService.GetNavigationItems(navigationPosition);
            if(listNavigation != null)
            {
                return View(viewName,listNavigation.ToList());
            }
            return View(viewName);
        }
    }
}
