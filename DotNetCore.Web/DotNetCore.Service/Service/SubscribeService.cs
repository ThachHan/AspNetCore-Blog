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
    public class SubscribeService : ISubscribeService
    {
        private readonly ISubscribeRepository _subscribesRepository;
        public SubscribeService(ISubscribeRepository subscribesRepository)
        {
            _subscribesRepository = subscribesRepository;
        }

        public async Task<bool> DeleteAsync(int subscribeId)
        {
            try
            {
                var Subscribe = await GetSubscribeById(subscribeId);
                await _subscribesRepository.Delete(Subscribe);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Subscribe> GetSubscribeById(int id)
        {
            return await _subscribesRepository.GetSubscribeById(id);
        }

        public IEnumerable<Subscribe> GetListSubscribes()
        {
            return _subscribesRepository.GetAll();
        }
        
        public bool Insert(Subscribe subscribe)
        {
            try
            {
                _subscribesRepository.Create(subscribe);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Subscribe subscribe)
        {
            try
            {
                _subscribesRepository.Update(subscribe);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
