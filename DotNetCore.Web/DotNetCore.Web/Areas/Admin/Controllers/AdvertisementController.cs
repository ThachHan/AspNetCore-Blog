using DotNetCore.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DotNetCore.Utility.ExtensionMethod;
using DotNetCore.Utility;
using DotNetCore.Data.Entity;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator,Writer,Approver")]
    public class AdvertisementController : AdminBaseController
    {
        #region field and constructor
        private readonly IAdvertisementService _advertisementService;
        private readonly IMapper _mapper;

        public AdvertisementController(IAdvertisementService advertisementServic, IMapper mapper)
        {
            _advertisementService = advertisementServic;
            _mapper = mapper;
        }
        #endregion

        // GET: Admin/Advertisement
        public ActionResult Index()
        {
            var viewModel = new List<AdvertisementViewModel>();
            var advertisements = _advertisementService.GetListAdvertisements();
            if (advertisements != null && advertisements.Any())
            {
                var model = new AdvertisementViewModel();
                foreach (var advertisement in advertisements)
                {
                    model = _mapper.Map<Advertisement, AdvertisementViewModel>(advertisement);
                    model.Status = advertisement.IsActive ? "Yes" : "No";
                    model.PositionAdvertisement = ((Common.AdvertisementPosition)Enum.ToObject(typeof(Common.AdvertisementPosition), advertisement.Position)).ToString();
                    viewModel.Add(model);
                }
            }
            return View(viewModel);
        }

        public ActionResult Create()
        {
            var allAdvertisementPositions = EnumUtil.GetValues<Common.AdvertisementPosition>().ToList();

            var viewModel = new AdvertisementViewModel()
            {
                AdvertisementPostions = new SelectList(allAdvertisementPositions)
            };

            return View(viewModel);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> CreateAsync(AdvertisementViewModel viewModel, IFormFile fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fileUpload == null || fileUpload.Length == 0)
                    return Content("file not selected");

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","images", fileUpload.FileName);
                if (!System.IO.File.Exists(path))
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await fileUpload.CopyToAsync(stream);
                    }
                }
                //insert ad to database
                var advertisement = _mapper.Map<AdvertisementViewModel, Advertisement>(viewModel);
                advertisement.Position = (int)Enum.Parse(typeof(Common.AdvertisementPosition), viewModel.PositionAdvertisement);
                advertisement.BannerUrl = "/images/" + viewModel.BannerUrl;
                if (_advertisementService.Insert(advertisement))
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Create");

        }

        public async System.Threading.Tasks.Task<ActionResult> DetailAsync(int id = 0)
        {
            var advertisement = await _advertisementService.GetAdvertisementById(id);
            if (advertisement != null)
            {
                AdvertisementViewModel viewModel = _mapper.Map<Advertisement, AdvertisementViewModel>(advertisement);
                viewModel.PositionAdvertisement = ((Common.AdvertisementPosition)Enum.ToObject(typeof(Common.AdvertisementPosition), advertisement.Position)).ToString();
                return View(viewModel);
            }
            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Index");
        }

        public async System.Threading.Tasks.Task<ActionResult> EditAsync(int id = 0)
        {
            var advertisement = await _advertisementService.GetAdvertisementById(id);
            if (advertisement != null)
            {
                AdvertisementViewModel viewModel = _mapper.Map<Advertisement, AdvertisementViewModel>(advertisement);
                viewModel.PositionAdvertisement = ((Common.AdvertisementPosition)Enum.ToObject(typeof(Common.AdvertisementPosition), advertisement.Position)).ToString();
                var allAdvertisementPositions = EnumUtil.GetValues<Common.AdvertisementPosition>().ToList();

                viewModel.AdvertisementPostions = new SelectList(allAdvertisementPositions);
                return View("Update",viewModel);
            }
            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> EditAsync(AdvertisementViewModel viewModel, IFormFile fileUpload)
        {
            if (ModelState.IsValid)
            {
                //insert ad to database
                var advertisement = _mapper.Map<AdvertisementViewModel, Advertisement>(viewModel);
                advertisement.Position = (int)Enum.Parse(typeof(Common.AdvertisementPosition), viewModel.PositionAdvertisement);

                if (fileUpload != null && fileUpload.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileUpload.FileName);
                    if (!System.IO.File.Exists(path))
                    {
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await fileUpload.CopyToAsync(stream);
                        }
                    }
                    advertisement.BannerUrl = "/images/" + viewModel.BannerUrl;
                }
                

                if (_advertisementService.Update(advertisement))
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, Constants.UnknowErrorMessage);
            return RedirectToAction("Update");

        }
    }
}