using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DotNetCore.Web.Models;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web
{
    [ViewComponent(Name = "Galery")]
    public class GaleryViewComponent : ViewComponent
    {
        private readonly IContentService _contentService;
        private readonly IMapper _mapper;
        public GaleryViewComponent(IContentService contentService, IMapper mapper)
        {
            _contentService = contentService;
            _mapper = mapper;
        }
        public IViewComponentResult Invoke()
        {
            var viewModel = new List<GalaryViewModel>();
            var listContents = _contentService.GetRecentsContent();
            if(listContents != null && listContents.Any())
            {
                viewModel = _mapper.Map<List<GalaryViewModel>>(listContents.Take(9));
            }
            return View(viewModel);
        }
    }
}
