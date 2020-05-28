using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.Data.Entity;
using DotNetCore.Data.Repositories;
using System.IO;
using DotNetCore.Utility;
using static DotNetCore.Utility.Common;

namespace DotNetCore.Service.Interface
{
    public interface IAdvertisementService
    {
        IEnumerable<Advertisement> GetAdvertisementsUnExpired();
        Task<Advertisement> GetAdvertisementById(int id);
        IEnumerable<Advertisement> GetListAdvertisements();
        Advertisement GetAdvertisement(AdvertisementPosition advertisementPosition);
        bool Update(Advertisement advertisement);
        bool Insert(Advertisement advertisement);
        bool Delete(int advertisementId);
    }
}
