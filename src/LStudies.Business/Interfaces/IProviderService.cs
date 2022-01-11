using LStudies.Business.Models;
using System;
using System.Threading.Tasks;

namespace LStudies.Business.Interfaces
{
    public interface IProviderService : IDisposable
    {
        Task Add(Provider provider);
        Task Update(Provider provider);
        Task Delete(Guid id);
        Task UpdateAddress(Address address);
    }
}
