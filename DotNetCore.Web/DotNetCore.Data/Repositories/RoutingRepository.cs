using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.DbManager;
using DotNetCore.Data.Entity;
using DotNetCore.Utility;
using Microsoft.EntityFrameworkCore;
using DotNetCore.Data.Interface;

namespace DotNetCore.Data.Repositories
{

    public class RoutingRepository : GenericRepository<DefineRouting>, IRoutingRepository
    {
        #region field and constructor
        private EntityCoreContext _database;
        public RoutingRepository(EntityCoreContext database) : base(database)
        {
            _database = database;
        }

        public DefineRouting GetDefineRouting(string controller, string action, int entityId)
        {
            return _database.DefineRouting.FirstOrDefault(n => n.Controller.Equals(controller, StringComparison.CurrentCultureIgnoreCase) && n.Action.Equals(action, StringComparison.CurrentCultureIgnoreCase) && n.EntityId == entityId);
        }

        public DefineRouting GetDefineRouting(string routingType, string friendlyUrl)
        {
            if (!string.IsNullOrEmpty(friendlyUrl) && !string.IsNullOrEmpty(routingType))
            {
                return _database.DefineRouting.FirstOrDefault(n => n.FriendlyUrlLv1.Equals(routingType, StringComparison.CurrentCultureIgnoreCase) && n.FriendlyUrlLv2.Equals(friendlyUrl, StringComparison.CurrentCultureIgnoreCase));
            }
            return null;
        }

        public DefineRouting GetDefineRouting(string routingType, int? entityId)
        {
            if (entityId.HasValue && !string.IsNullOrEmpty(routingType))
            {
                return _database.DefineRouting.FirstOrDefault(n => n.Controller.Equals(routingType, StringComparison.CurrentCultureIgnoreCase) && n.EntityId == entityId.Value);
            }
            return null;
        }

        public async Task<DefineRouting> GetDefineRoutingById(int id)
        {
            return await _database.DefineRouting.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        }

        public bool IsExistRoutingFriendlyUrl(Common.RoutingType routingType, string friendlyUrl)
        {
            if (_database.DefineRouting.FirstOrDefault(n => n.FriendlyUrlLv1.Equals(routingType.ToString(), StringComparison.CurrentCultureIgnoreCase) && n.FriendlyUrlLv2.Equals(friendlyUrl, StringComparison.CurrentCultureIgnoreCase)) != null)
                return true;
            return false;
        }
        #endregion
    }
}
