using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.DbManager;
using DotNetCore.Data.Entity;
using Microsoft.EntityFrameworkCore;
namespace DotNetCore.Data.Interface
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Tag GetTagById(int id);
        bool RemoveListTag(IEnumerable<Tag> tags);
    }

}
