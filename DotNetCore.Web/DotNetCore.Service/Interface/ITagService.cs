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
    public interface ITagService
    {
        Tag GetTagById(int id);
        IEnumerable<Tag> GetListTags();
        bool Update(Tag tag);
        bool Insert(Tag tag, int userId);
        bool InsertMap(ContentTagMap contentTagMap);
        IEnumerable<Tag> GetTagByContent(int contentId);
        Tag GetTagByTagUrl(string tagUrl);
        Tag GetTagByTagName(string tagName);
        bool CreateTagForContent(int contentId, int[] tags);
    }
}
