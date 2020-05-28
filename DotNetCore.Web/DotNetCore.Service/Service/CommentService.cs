using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.Data.Entity;
using DotNetCore.Data.Repositories;
using System.IO;
using DotNetCore.Utility;
using DotNetCore.Service.Interface;
using DotNetCore.Data.Interface;
namespace DotNetCore.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public bool Delete(int commentId)
        {
            try
            {
                var comment = GetCommentById(commentId);
                _commentRepository.Delete(comment.Result);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await _commentRepository.GetCommentById(id);
        }

        public IEnumerable<Comment> GetListComment()
        {
            return _commentRepository.GetListComment();
        }

        public async Task<IEnumerable<Comment>> GetListCommentByContent(int contentId)
        {
            return await _commentRepository.GetListCommentsByContent(contentId);
        }

        public async Task<IEnumerable<Comment>> GetListCommentByParent(int commentId)
        {
            return await _commentRepository.GetListCommentsByParent(commentId);
        }

        public async Task<IEnumerable<Comment>> GetListShowedCommentByContent(int contentId)
        {
            return await _commentRepository.GetListCommentsIsShowByContent(contentId);
        }

        public async Task<IEnumerable<Comment>> GetListShowedCommentByParent(int commentId)
        {
            return await _commentRepository.GetListCommentsIsShowByParent(commentId);
        }

        public async Task<int> GetTotalCommentByContentAsync(int contentId)
        {
            var result = 0;
            var comments = await _commentRepository.GetListCommentsIsShowByContent(contentId);
            if(comments != null && comments.Any())
            {
                result = comments.Count();
                foreach (var comment in comments)
                {
                    var commentChilds = await _commentRepository.GetListCommentsIsShowByParent(comment.Id);
                    if (commentChilds != null && commentChilds.Any())
                    {
                        result += commentChilds.Count();
                    }
                }
            }
            return result;
        }

        public bool Insert(Comment comment)
        {
            try
            {
                _commentRepository.Create(comment);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Comment comment)
        {
            try
            {
                _commentRepository.Update(comment);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
