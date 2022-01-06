using LStudies.Business.Models;
using System;
using System.Threading.Tasks;

namespace LStudies.Business.Interfaces
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Task<Provider> GetProviderAdress(Guid id);
        Task<Provider> GetProviderProductsAdress(Guid id);
    }
}
