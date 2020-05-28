using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DotNetCore.Data.Repositories;
using DotNetCore.Data.Models;
using DotNetCore.Data.Entity;
using DotNetCore.Utility;

namespace DotNetCore.Service.Interface
{
    public interface INavigationService
    {
        IEnumerable<NavigationItem> GetNavigationParents();
        IList<NavigationItem> GetNavigationChilds(int parentId, Common.NavigationPosition navigationPosition);
        IList<NavigationItem> GetNavigationItems(Common.NavigationPosition navigationPosition);
    }
}
