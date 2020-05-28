using DotNetCore.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.DbManager;
using Microsoft.EntityFrameworkCore;
using DotNetCore.Utility;

namespace DotNetCore.Data.Interface
{
    public interface IContentRepository : IGenericRepository<Content>
    {
        Content GetContentByTitle(string title);
        Task<Content> GetContentById(int Id);
        IEnumerable<Content> GetListContentsByStatus(Common.ContentStatus contentStatus);
        Task<IEnumerable<Content>> SearchContent(string key);
    }

}
