using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.DbManager;
using DotNetCore.Data.Entity;
using DotNetCore.Utility;
using Microsoft.EntityFrameworkCore;


namespace DotNetCore.Data.Interface
{
    public interface IRoutingRepository : IGenericRepository<DefineRouting>
    {
        DefineRouting GetDefineRouting(string controller, string action, int entityId);
        DefineRouting GetDefineRouting(string routingType, string friendlyUrl);
        DefineRouting GetDefineRouting(string routingType, int? entityId);
        bool IsExistRoutingFriendlyUrl(Common.RoutingType routingType, string friendlyUrl);
        Task<DefineRouting> GetDefineRoutingById(int id);
    }

}
