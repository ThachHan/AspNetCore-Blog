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
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IContentTagMapRepository _contentTagMapRepository;
        private readonly IRoutingService _routingService;
        public TagService(ITagRepository tagRepository, IContentTagMapRepository contentTagMapRepository,
            IRoutingService routingService)
        {
            _tagRepository = tagRepository;
            _contentTagMapRepository = contentTagMapRepository;
            _routingService = routingService;
        }


        public bool CreateTagForContent(int contentId, int[] tags)
        {
            try
            {
                if (_contentTagMapRepository.DeleteMap(contentId))
                {
                    foreach (var tag in tags)
                    {
                        var entityTag = GetTagById(tag);
                        if (entityTag != null && _contentTagMapRepository.GetContentTagMap(contentId, entityTag.TagId) == null)
                        {
                            var contentTagmap = new ContentTagMap()
                            {
                                ContentId = contentId,
                                TagId = entityTag.TagId
                            };
                            _contentTagMapRepository.Create(contentTagmap);
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Tag GetTagById(int id)
        {
            return _tagRepository.GetTagById(id);
        }

        public IEnumerable<Tag> GetListTags()
        {
            return _tagRepository.GetAll();
        }

        public bool Insert(Tag tag, int userId)
        {
            try
            {
                _tagRepository.Create(tag);
                _routingService.Insert(Common.RoutingType.Tag, userId, tag: tag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Tag tag)
        {
            try
            {
                _tagRepository.Update(tag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertMap(ContentTagMap contentTagMap)
        {
            try
            {
                _contentTagMapRepository.Create(contentTagMap);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Tag> GetTagByContent(int contentId)
        {
            return _contentTagMapRepository.GetListMapTagByContent(contentId).Select(n => n.Tag);
        }

        public Tag GetTagByTagUrl(string tagUrl)
        {
            return _tagRepository.GetAll().FirstOrDefault(n => n.TagUrl.Equals(tagUrl, StringComparison.InvariantCultureIgnoreCase));
        }

        public Tag GetTagByTagName(string tagName)
        {
            return _tagRepository.GetAll().FirstOrDefault(n => n.TagName.Equals(tagName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
