using AutoMapper;
using DotNetCore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web.Controllers
{
    public class NewsLettersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ISubscribeService _subscribeService;
        public NewsLettersController(IMapper mapper, ISubscribeService subscribeService)
        {
            _mapper = mapper;
            _subscribeService = subscribeService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SaveSubscribe(string email)
        {
            if (ModelState.IsValid)
            {
                var subscribe = new Subscribe()
                {
                    Email = email
                };
                _subscribeService.Insert(subscribe);
                return Json(true);
            }
            return Json(false);
        }
    }
}