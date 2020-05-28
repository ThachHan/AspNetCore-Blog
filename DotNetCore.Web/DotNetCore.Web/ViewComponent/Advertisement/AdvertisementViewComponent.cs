using Microsoft.AspNetCore.Mvc;
using DotNetCore.Utility;
using DotNetCore.Web.Models;
using AutoMapper;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web
{
    [ViewComponent(Name = "Advertisement")]
    public class AdvertisementViewComponent : ViewComponent
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IMapper _mapper;
        public AdvertisementViewComponent(IAdvertisementService advertisementService, IMapper mapper)
        {
            _advertisementService = advertisementService;
            _mapper = mapper;
        }
        public IViewComponentResult Invoke(Common.AdvertisementPosition advertisementPosition)
        {
            var advertisement = _advertisementService.GetAdvertisement(advertisementPosition);
            AdvertisementViewModel viewModel = null;
            var viewName = advertisementPosition == Common.AdvertisementPosition.BodyTop ? "TopAdvertisement" : (advertisementPosition == Common.AdvertisementPosition.BodyCenter ? "CenterAdvertisement" : "BottomAdvertisement");
            if (advertisement != null)
            {
                viewModel = _mapper.Map<AdvertisementViewModel>(advertisement);
            }
            return View(viewName, viewModel);
        }
    }
}
