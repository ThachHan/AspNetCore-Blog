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

    public interface IContentService
    {
        IEnumerable<Content> GetListContentsByCategory(int categoryId, Common.ContentStatus contentStatus);
        IEnumerable<Content> GetListContentOfAuthor(int authorId, Common.ContentStatus contentStatus);
        IEnumerable<Content> GetListContentByTag(int tagId, Common.ContentStatus contentStatus);
        IEnumerable<Content> GetListContentsByStatus(Common.ContentStatus contentStatus);
        Task<Content> GetContentById(int id);
        IEnumerable<Content> GetListContent();
        IEnumerable<Content> GetListContentShowBanner();
        IEnumerable<Content> GetPopularContent();
        IEnumerable<Content> GetRecentsContent();
        Task<IEnumerable<Content>> GetListLatedPostAsync(int contentId, int number);
        Task<IEnumerable<Content>> SearchContent(string key);
        bool InsertMapContentCategories(int contentId, int[] categories);
        int GetPostTotalForCategory(int categoryId);
        bool DeleteMapContent(int contentId);
        bool Update(Content content);
        bool Update(Content content, int[] tags);
        bool Insert(Content content, int userId, int[] tags);
        bool Delete(int contentId);
        bool IsExistContentName(string Title, int? Id);
        bool IncreaseCounterContent(Content content);
        DateTime GetDatePublishByContent(int contentId);
        bool IsCategoryHasContent(int categoryId);
    }

}
