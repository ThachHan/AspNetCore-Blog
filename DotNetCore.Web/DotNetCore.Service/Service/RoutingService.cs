using DotNetCore.Data;
using DotNetCore.Data.Entity;
using System;
using System.Collections.Generic;
using DotNetCore.Utility.ExtensionMethod;
using System.Linq;
using DotNetCore.Data.Repositories;
using System.Threading.Tasks;
using DotNetCore.Utility;
using DotNetCore.Service.Interface;
using DotNetCore.Data.Interface;
namespace DotNetCore.Service
{
    public class RoutingService : IRoutingService
    {
        #region Fields and Constructors
        private readonly IRoutingRepository _routingRepository;

        public RoutingService(IRoutingRepository routingRepository)
        {
            this._routingRepository = routingRepository;
        }
        #endregion

        #region Implement
        public IEnumerable<DefineRouting> GetListRouting()
        {
            return _routingRepository.GetAll();
        }

        public async Task<DefineRouting> GetDefineRoutingById(int id)
        {
            return await _routingRepository.GetDefineRoutingById(id);
        }

        public bool IsExistRoutingFriendlyUrl(Common.RoutingType routingType, string friendlyUrl)
        {
            return _routingRepository.IsExistRoutingFriendlyUrl(routingType, friendlyUrl);
        }

        public bool Insert(Common.RoutingType routingType, int userId, Category category = null, Content content = null, Author author = null, Tag tag = null)
        {
            try
            {
                var defineRouting = new DefineRouting();
                switch(routingType)
                {
                    case Common.RoutingType.Category:
                        {
                            defineRouting.Controller = Constants.CategoryController;
                            defineRouting.Action = Constants.CategoryAction;
                            defineRouting.FriendlyUrlLv2 = NormalizeFriendlyUrl(routingType, ExtensionMethod.GenerateSlug(category.Title));
                            defineRouting.EntityId = category.CategoryId;
                            break;
                        }
                    case Common.RoutingType.Content:
                        {
                            defineRouting.Controller = Constants.ContentController;
                            defineRouting.Action = Constants.ContentAction;
                            defineRouting.FriendlyUrlLv2 = NormalizeFriendlyUrl(routingType, ExtensionMethod.GenerateSlug(content.Title));
                            defineRouting.EntityId = content.ContentId;
                            break;
                        }
                    case Common.RoutingType.Author:
                        {
                            defineRouting.Controller = Constants.AuthorController;
                            defineRouting.Action = Constants.AuthorAction;
                            defineRouting.FriendlyUrlLv2 = NormalizeFriendlyUrl(routingType, ExtensionMethod.GenerateSlug(string.Format("{0} {1}",author.FirstName.Trim(), author.LastName.Trim())));
                            defineRouting.EntityId = author.AuthorId;
                            break;
                        }
                    case Common.RoutingType.Tag:
                        {
                            defineRouting.Controller = Constants.TagController;
                            defineRouting.Action = Constants.TagAction;
                            defineRouting.FriendlyUrlLv2 = NormalizeFriendlyUrl(routingType, ExtensionMethod.GenerateSlug(tag.TagName));
                            defineRouting.EntityId = tag.TagId;
                            break;
                        }
                }
                defineRouting.FriendlyUrlLv1 = routingType.ToString().ToLower();
                defineRouting.CreatedUserId = defineRouting.UpdatedUserId = userId;
                _routingRepository.Create(defineRouting);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        

        private string NormalizeFriendlyUrl(Common.RoutingType routingType, string friendlyUrl)
        {
            friendlyUrl = friendlyUrl.Replace('/',' ').Trim();

            if (friendlyUrl.Length >= 128)
            {
                friendlyUrl = friendlyUrl.Substring(0, 115);
            }
            var isExistFriendUrl = IsExistRoutingFriendlyUrl(routingType, friendlyUrl);
            if (isExistFriendUrl)
            {
                friendlyUrl = friendlyUrl + Constants.TruncateContent;
            }
            return friendlyUrl;
        }

        public string GetRoutingByUrl(string url)
        {
            if(!string.IsNullOrEmpty(url))
            {
                var listFriendlyUrl = url.Split('/');
                if (listFriendlyUrl.Count() == 3)
                {
                    if (IsContainsSlug(listFriendlyUrl[1]))
                    {
                        var defineRouting = _routingRepository.GetDefineRouting(listFriendlyUrl[1], listFriendlyUrl[2]);
                        if (defineRouting != null)
                            return string.Format("{0}/{1}/{2}/{3}", listFriendlyUrl[0], defineRouting.Controller, defineRouting.Action, defineRouting.EntityId);
                    }
                }
                return url;
            }
            return url;
        }

        private bool IsContainsSlug(string path)
        {
            return path.Equals(Constants.CategoryController, StringComparison.CurrentCultureIgnoreCase)
                || path.Equals(Constants.ContentController, StringComparison.CurrentCultureIgnoreCase)
                || path.Equals(Constants.AuthorController, StringComparison.CurrentCultureIgnoreCase)
                || path.Equals(Constants.TagController, StringComparison.CurrentCultureIgnoreCase);
        }

        public DefineRouting GetDefineRouting(string controller, string action, int entityId)
        {
            return _routingRepository.GetDefineRouting(controller, action, entityId);
        }

        public bool Delete(int id)
        {
            try
            {
                var routing = GetDefineRoutingById(id);
                _routingRepository.Delete(routing.Result);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetUrl(Common.RoutingType routingType, int entityId)
        {
            var routing = _routingRepository.GetDefineRouting(routingType.ToString(), entityId);
            if(routing != null)
            {
                var url = string.Format(@"/{0}/{1}", routing.FriendlyUrlLv1, routing.FriendlyUrlLv2);
                return url;
            }
            return null;
        }

        public bool Update(DefineRouting defineRouting)
        {
            try
            {
                _routingRepository.Update(defineRouting);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
