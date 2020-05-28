using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.Data.Entity;
using DotNetCore.Data.Repositories;
using System.IO;
using DotNetCore.Utility;
using static DotNetCore.Utility.Common;
using DotNetCore.Service.Interface;
using DotNetCore.Data.Interface;
namespace DotNetCore.Service
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _AdvertisementsRepository;
        public AdvertisementService(IAdvertisementRepository AdvertisementsRepository)
        {
            _AdvertisementsRepository = AdvertisementsRepository;
        }

        public bool Delete(int AdvertisementId)
        {
            try
            {
                var advertisement = GetAdvertisementById(AdvertisementId);
                _AdvertisementsRepository.Delete(advertisement.Result);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Advertisement GetAdvertisement(AdvertisementPosition advertisementPosition)
        {
            return GetAdvertisementsUnExpired().Where(n => n.IsActive && n.Position == (int)advertisementPosition).OrderByDescending(n=>n.CreatedDate).FirstOrDefault();
        }

        public async Task<Advertisement> GetAdvertisementById(int id)
        {
            return await _AdvertisementsRepository.GetAdvertisementById(id);
        }

        public IEnumerable<Advertisement> GetAdvertisementsUnExpired()
        {
            return GetListAdvertisements().Where(n => n.DateExpired == null || n.DateExpired.Value >= DateTime.Now);
        }

        public IEnumerable<Advertisement> GetListAdvertisements()
        {
            return _AdvertisementsRepository.GetAll();
        }
        
        public bool Insert(Advertisement advertisement)
        {
            try
            {
                _AdvertisementsRepository.Create(advertisement);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Advertisement advertisement)
        {
            try
            {
                _AdvertisementsRepository.Update(advertisement);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }       
    }
}
