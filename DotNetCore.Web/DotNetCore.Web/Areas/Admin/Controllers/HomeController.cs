using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace DotNetCore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator,Writer,Approver")]
    public class HomeController : AdminBaseController
    {
        [HttpGet]
        public ActionResult Index()
        {           
            return View("Index");
        }
    }
}