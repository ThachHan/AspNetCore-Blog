using DotNetCore.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.DbManager;
using Microsoft.EntityFrameworkCore;

using static DotNetCore.Utility.Common;

namespace DotNetCore.Data.Interface
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetListCommentsByContent(int contentId);
        Task<IEnumerable<Comment>> GetListCommentsIsShowByContent(int contentId);
        Task<IEnumerable<Comment>> GetListCommentsByParent(int commentId);
        Task<IEnumerable<Comment>> GetListCommentsIsShowByParent(int commentId);
        Task<Comment> GetCommentById(int id);
        IEnumerable<Comment> GetListComment();
    }


}
