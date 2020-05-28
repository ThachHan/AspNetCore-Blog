using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminBaseController : Controller
    {
        protected int _pageSize { get; set; }

        public AdminBaseController()
        {
            _pageSize = 10;
        }
    }
}