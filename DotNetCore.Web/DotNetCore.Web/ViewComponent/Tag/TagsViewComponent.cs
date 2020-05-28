using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Utility;
using AutoMapper;
using DotNetCore.Web.Models;
using DotNetCore.Service.Interface;
namespace DotNetCore.Web
{
    [ViewComponent(Name = "Tags")]
    public class TagsViewComponent : ViewComponent
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;
        private readonly IRoutingService _routingService;
        public TagsViewComponent(ITagService tagService, IMapper mapper, IRoutingService routingService)
        {
            _tagService = tagService;
            _mapper = mapper;
            _routingService = routingService;
        }
        public IViewComponentResult Invoke(Common.TagPosition tagPosition )
        { 
            var tags = _tagService.GetListTags();
            var viewName = tagPosition == Common.TagPosition.Body ? "TagsBody" : "TagsFooter";
            List<TagViewModel> viewModel = null;
            if (tags != null && tags.Count() > 0)
            {                
                viewModel = _mapper.Map<List<TagViewModel>>(tags.ToList());
                foreach (var tag in viewModel)
                {
                    tag.TagUrl = _routingService.GetUrl(Common.RoutingType.Tag, tag.TagId);
                }
            }
            return View(viewName, viewModel);
        }
    }
}
