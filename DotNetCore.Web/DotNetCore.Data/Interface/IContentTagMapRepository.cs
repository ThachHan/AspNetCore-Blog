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
    public interface IContentTagMapRepository : IGenericRepository<ContentTagMap>
    {
        ContentTagMap GetContentTagMap(int contentId, int tagId);
        IEnumerable<ContentTagMap> GetListMapContentsByTag(int tagId, Common.ContentStatus contentStatus);
        IEnumerable<ContentTagMap> GetListMapTagByContent(int contentId);
        bool DeleteMap(int contentId);
    }
}
