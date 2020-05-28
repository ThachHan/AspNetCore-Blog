using DotNetCore.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.DbManager;
using DotNetCore.Utility;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.Data.Interface
{
    public interface IContentCategoryMapRepository : IGenericRepository<ContentCategoryMap>
    {
        IEnumerable<ContentCategoryMap> GetListMapCategoryByContent(int contentId);
        IEnumerable<ContentCategoryMap> GetListMapContentsByCategory(int categoryId, Common.ContentStatus contentStatus);
        IEnumerable<ContentCategoryMap> GetListMapContentsByStatus(Common.ContentStatus contentStatus);
        bool DeleteMapContentCategory(int contentId);
    }

}
