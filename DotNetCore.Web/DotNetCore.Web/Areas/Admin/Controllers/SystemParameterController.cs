using DotNetCore.Web.Areas.Admin.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Data.Entity;
using DotNetCore.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using DotNetCore.Utility.ExtensionMethod;
using System;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SystemParameterController : AdminBaseController
    {
        private readonly ISystemService _systemService;
        private readonly IMapper _mapper;

        public SystemParameterController(ISystemService systemService, IMapper mapper)
        {
            _systemService = systemService;
            _mapper = mapper;
        }

        public ActionResult Index(string msg = "")
        {
            ViewBag.MessageAlert = msg;
            List<SystemParameterViewModel> viewModel = null;
            var systemParameters = _systemService.GetAll();
            if (systemParameters != null && systemParameters.Any())
            {
                viewModel = _mapper.Map<List<SystemParameter>, List<SystemParameterViewModel>>(systemParameters.ToList());
            }
            return View(viewModel);
        }


        public async System.Threading.Tasks.Task<ActionResult> EditAsync(int? id)
        {
            if (id.HasValue)
            {
                var systemParameter = await _systemService.GetById(id.Value);
                if (systemParameter != null)
                {
                    var viewModel = _mapper.Map<SystemParameter, SystemParameterViewModel>(systemParameter);
                    if (viewModel != null)
                    {
                        return View("Edit", viewModel);
                    }
                }
            }

            return null;
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> EditAsync(SystemParameterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var systemParameter = await _systemService.GetById(viewModel.Id);
                if (systemParameter.SystemParameterName == viewModel.SystemParameterName)
                {
                    var newSysPara = _mapper.Map<SystemParameterViewModel, SystemParameter>(viewModel);
                    _systemService.Update(newSysPara);

                    return RedirectToAction("Index", new { @msg = Constants.UpdateSuccessfully });
                }
            }
            return View(viewModel);
        }


        public ActionResult Create()
        {
            var systemParametes = _systemService.GetAll().Select(n => n.SystemParameterName).Select(x => Enum.Parse<Common.SystemParameterType>(x));
            var allSystemParameters = EnumUtil.GetValues<Common.SystemParameterType>().ToList().Except(systemParametes.ToList());

            var viewModel = new SystemParameterViewModel()
            {
                SystemParamaterNames = new SelectList(allSystemParameters)
            };

            return View(viewModel);
        }
        // POST: Admin/SystemPara/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(SystemParameterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var systemParameter = _mapper.Map<SystemParameterViewModel, SystemParameter>(viewModel);
                _systemService.Insert(systemParameter);

                return RedirectToAction("Index", new { @msg = Constants.InsertSuccessfully });
            }
            return View(viewModel);
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}