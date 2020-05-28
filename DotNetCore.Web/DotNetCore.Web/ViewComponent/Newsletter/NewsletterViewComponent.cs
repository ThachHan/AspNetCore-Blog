using Microsoft.AspNetCore.Mvc;
using DotNetCore.Utility;
using DotNetCore.Web.Models;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web
{
    [ViewComponent(Name = "Newsletter")]
    public class NewsletterViewComponent : ViewComponent
    {
        private readonly ISystemService _systemService;
        public NewsletterViewComponent(ISystemService systemService)
        {
            _systemService = systemService;
        }
        public IViewComponentResult Invoke(Common.NewsletterPosition newsletterPosition)
        {
            var viewName = newsletterPosition == Common.NewsletterPosition.Body ? "NewsletterBody" : "NewsletterFooter";
            return View(viewName, new SubscribeViewModel());
        }
    }
}
