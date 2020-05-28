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
    public class ContentService : IContentService
    {
        private readonly IContentRepository _contentRepository;
        private readonly IContentCategoryMapRepository _contentCategoryMapRepository;
        private readonly IContentTagMapRepository _contentTagMapRepository;
        private readonly IRoutingService _routingService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public ContentService(IContentRepository contentRepository, IContentCategoryMapRepository contentCategoryMapRepository,
            IRoutingService routingService, ICategoryService groupService, IContentTagMapRepository contentTagMapRepository,
            ITagService tagService)
        {
            _contentCategoryMapRepository = contentCategoryMapRepository;
            _categoryService = groupService;
            _routingService = routingService;
            _contentRepository = contentRepository;
            _contentTagMapRepository = contentTagMapRepository;
            _tagService = tagService;
        }

        public IEnumerable<Content> GetContentByStatus(Common.ContentStatus contentStatus)
        {
            return _contentRepository.GetAll().Where(n => n.PostStatus == (int)contentStatus);
        }

        public async Task<Content> GetContentById(int id)
        {
            return await _contentRepository.GetContentById(id);
        }
        public bool Update(Content content, int[] tags)
        {
            try
            {
                _contentRepository.Update(content);
                if (tags != null && tags.Any())
                {
                    _tagService.CreateTagForContent(content.ContentId, tags);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Content> GetListContentsByCategory(int categoryId, Common.ContentStatus contentStatus)
        {
            return _contentCategoryMapRepository.GetListMapContentsByCategory(categoryId, contentStatus).Select(n => n.Content).OrderByDescending(n=>n.ContentId);
        }

        public IEnumerable<Content> GetListContentsByStatus(Common.ContentStatus contentStatus)
        {
            return _contentRepository.GetListContentsByStatus(contentStatus);
        }

        public IEnumerable<Content> GetListContent()
        {
            return _contentRepository.GetAll();
        }
        public bool Insert(Content content, int userId, int[] tags)
        {
            try
            {
                _contentRepository.Create(content);
                _routingService.Insert(Common.RoutingType.Content, userId, content: content);
                if (tags != null && tags.Any())
                {
                    _tagService.CreateTagForContent(content.ContentId, tags);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int contentId)
        {
            try
            {
                var content = GetContentById(contentId);
                _contentRepository.Delete(content.Result);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsExistContentName(string Title, int? Id)
        {
            Title = Title.Trim();

            var isExist = _contentRepository.GetContentByTitle(Title);
            if (isExist != null)
            {
                if (Id.HasValue && isExist.ContentId == Id.Value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public IEnumerable<Content> GetListContentShowBanner()
        {
            return _contentRepository.GetAll().Where(n => n.IsShowBanner && n.PostStatus == (int)Common.ContentStatus.Published).OrderByDescending(n => n.ContentId);
        }

        public IEnumerable<Content> GetPopularContent()
        {
            return _contentRepository.GetAll().Where(n => n.PostStatus == (int)Common.ContentStatus.Published).OrderByDescending(n => n.Counter);
        }

        public bool IncreaseCounterContent(Content content)
        {
            try
            {
                content.Counter++;
                _contentRepository.Update(content);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Content>> GetListLatedPostAsync(int contentId, int number)
        {
            var content = await GetContentById(contentId);
            if (content != null)
            {
                var contentCategoryMap = _contentCategoryMapRepository.GetAll().FirstOrDefault(n => n.ContentId == contentId);
                if (contentCategoryMap != null)
                {
                    return GetListContentsByCategory(contentCategoryMap.CategoryId, Common.ContentStatus.Published).Where(n => n.ContentId != contentId).Take(number);
                }
            }
            return null;
        }

        public IEnumerable<Content> GetListContentOfAuthor(int authorId, Common.ContentStatus contentStatus)
        {
            return GetListContent().Where(n => n.AuthorId == authorId && n.PostStatus == (int)contentStatus);
        }

        public IEnumerable<Content> GetListContentByTag(int tagId, Common.ContentStatus contentStatus)
        {
            return _contentTagMapRepository.GetListMapContentsByTag(tagId, contentStatus).Select(n => n.Content);
        }

        public bool DeleteMapContent(int contentId)
        {
            return _contentCategoryMapRepository.DeleteMapContentCategory(contentId);
        }

        public bool InsertMapContentCategories(int contentId, int[] categories)
        {
            try
            {
                if (DeleteMapContent(contentId))
                {
                    foreach (var category in categories)
                    {
                        var contentMap = new ContentCategoryMap()
                        {
                            ContentId = contentId,
                            CategoryId = category,
                        };
                        _contentCategoryMapRepository.Create(contentMap);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetPostTotalForCategory(int categoryId)
        {
            var listContent = GetListContentsByCategory(categoryId, Common.ContentStatus.Published);
            var result = listContent != null && listContent.Any() ? listContent.Count() : 0;
            return result;
        }

        public bool Update(Content content)
        {
            try
            {
                _contentRepository.Update(content);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DateTime GetDatePublishByContent(int contentId)
        {
            var contentCategoryMap = _contentCategoryMapRepository.GetListMapCategoryByContent(contentId);
            if (contentCategoryMap != null && contentCategoryMap.Any())
            {
                return contentCategoryMap.FirstOrDefault().CreatedDate;
            }
            return DateTime.Now;
        }

        public IEnumerable<Content> GetRecentsContent()
        {
            return _contentRepository.GetAll().Where(n => n.PostStatus == (int)Common.ContentStatus.Published).OrderByDescending(n => n.ContentId);
        }

        public bool IsCategoryHasContent(int categoryId)
        {
            var contents = _contentCategoryMapRepository.GetListMapContentsByCategory(categoryId,Common.ContentStatus.Published);
            return contents != null && contents.Any();
        }

        public async Task<IEnumerable<Content>> SearchContent(string key)
        {
            return await _contentRepository.SearchContent(key);
        }
    }
}
