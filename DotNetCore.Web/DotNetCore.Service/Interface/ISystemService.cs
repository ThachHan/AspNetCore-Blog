using DotNetCore.Data.Repositories;
using DotNetCore.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using DotNetCore.Utility;
using System.Threading.Tasks;

namespace DotNetCore.Service.Interface
{
    public interface ISystemService
    {
        IEnumerable<SystemParameter> GetAll();
        Task<SystemParameter> GetById(int id);
        SystemParameter GetByName(string name);
        bool Update(SystemParameter sysPara);
        bool Insert(SystemParameter sysPara);
        bool Delete(int id);
        string GetSystemParameter(Utility.Common.SystemParameterType type);
    }
}
