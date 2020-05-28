using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.Data.Entity;
using DotNetCore.Data.Repositories;
using System.IO;
using DotNetCore.Utility;

namespace DotNetCore.Service.Interface
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetListCommentByContent(int contentId);
        Task<IEnumerable<Comment>> GetListCommentByParent(int commentId);
        Task<IEnumerable<Comment>> GetListShowedCommentByContent(int contentId);
        Task<IEnumerable<Comment>> GetListShowedCommentByParent(int commentId);
        Task<Comment> GetCommentById(int id);
        IEnumerable<Comment> GetListComment();
        Task<int> GetTotalCommentByContentAsync(int contentId);
        bool Update(Comment comment);
        bool Insert(Comment comment);
        bool Delete(int commentId);
    }

}
