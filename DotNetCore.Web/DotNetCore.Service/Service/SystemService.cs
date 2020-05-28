using DotNetCore.Data.Repositories;
using DotNetCore.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using DotNetCore.Utility;
using System.Threading.Tasks;
using DotNetCore.Service.Interface;
using DotNetCore.Data.Interface;
namespace DotNetCore.Service
{
    public class SystemService : ISystemService
    {
        private readonly ISystemParameterRepository _sysParaRepository;

        public SystemService(ISystemParameterRepository sysParaRepository)
        {
            _sysParaRepository = sysParaRepository;
        }

        public bool Delete(int id)
        {
            try
            {
                var systemParameter = _sysParaRepository.GetSystemParameterById(id);
                _sysParaRepository.Delete(systemParameter.Result);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<SystemParameter> GetAll()
        {
            return _sysParaRepository.GetAll();
        }

        public async Task<SystemParameter> GetById(int id)
        {
            return await _sysParaRepository.GetSystemParameterById(id);
        }

        public SystemParameter GetByName(string name)
        {
            return GetAll().FirstOrDefault(n => n.SystemParameterName.Trim().Equals(name.Trim(), StringComparison.InvariantCultureIgnoreCase));
        }

        public string GetSystemParameter(Common.SystemParameterType type)
        {
            var systemParameter = GetByName(type.ToString());
            return systemParameter != null ? systemParameter.SystemParameterValue : string.Empty;
        }

        public bool Insert(SystemParameter sysPara)
        {
            try
            {
                _sysParaRepository.Create(sysPara);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(SystemParameter sysPara)
        {
            try
            {
                _sysParaRepository.Update(sysPara);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
