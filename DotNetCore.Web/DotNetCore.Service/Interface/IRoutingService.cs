using DotNetCore.Data;
using DotNetCore.Data.Entity;
using System;
using System.Collections.Generic;
using DotNetCore.Utility.ExtensionMethod;
using System.Linq;
using DotNetCore.Data.Repositories;
using System.Threading.Tasks;
using DotNetCore.Utility;

namespace DotNetCore.Service.Interface
{
    public interface IRoutingService
    {
        IEnumerable<DefineRouting> GetListRouting();
        Task<DefineRouting> GetDefineRoutingById(int id);
        string GetRoutingByUrl(string url);
        string GetUrl(Common.RoutingType routingType, int entityId);
        DefineRouting GetDefineRouting(string controller, string action, int entityId);
        bool IsExistRoutingFriendlyUrl(Common.RoutingType routingType, string friendlyUrl);
        bool Insert(Common.RoutingType routingType, int userId, Category category = null, Content content = null, Author author = null, Tag tag = null);
        bool Delete(int id);
        bool Update(DefineRouting defineRouting);
    }

}
