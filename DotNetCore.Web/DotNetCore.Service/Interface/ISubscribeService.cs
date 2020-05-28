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
    public interface ISubscribeService
    {
        Task<Subscribe> GetSubscribeById(int id);
        IEnumerable<Subscribe> GetListSubscribes();
        bool Update(Subscribe subscribe);
        bool Insert(Subscribe subscribe);
        Task<bool> DeleteAsync(int subscribeId);
    }

}
