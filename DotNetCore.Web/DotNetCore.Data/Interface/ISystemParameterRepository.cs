using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DotNetCore.Data.DbManager;
using DotNetCore.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.Data.Interface
{
    public interface ISystemParameterRepository : IGenericRepository<SystemParameter>
    {
        Task<SystemParameter> GetSystemParameterById(int id);
    }

}
