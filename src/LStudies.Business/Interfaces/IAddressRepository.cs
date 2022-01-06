using LStudies.Business.Models;
using System;
using System.Threading.Tasks;

namespace LStudies.Business.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressByProvider(Guid providerId);
    }
}
