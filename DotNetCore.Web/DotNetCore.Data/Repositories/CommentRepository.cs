using DotNetCore.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Data.DbManager;
using Microsoft.EntityFrameworkCore;
using DotNetCore.Data.Interface;

using static DotNetCore.Utility.Common;

namespace DotNetCore.Data.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        #region field and constructor
        private readonly EntityCoreContext _database;
        public CommentRepository(EntityCoreContext database) : base(database)
        {
            _database = database;
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await _database.Comment.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        }

        public IEnumerable<Comment> GetListComment()
        {
            return GetAll();
        }

        public async Task<IEnumerable<Comment>> GetListCommentsByContent(int contentId)
        {
            return await _database.Comment.Where(n => n.ContentId == contentId && (n.CommentParentId == 0 || n.CommentParentId == null)).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetListCommentsByParent(int commentId)
        {
            return await _database.Comment.Where(n => n.CommentParentId == commentId).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetListCommentsIsShowByContent(int contentId)
        {
            return await _database.Comment.Where(n=>n.ContentId == contentId && n.IsActive && (n.CommentParentId == 0 || n.CommentParentId == null)).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetListCommentsIsShowByParent(int commentId)
        {
            return await _database.Comment.Where(n => n.CommentParentId == commentId && n.IsActive).ToListAsync();
        }
        #endregion

    }
}
