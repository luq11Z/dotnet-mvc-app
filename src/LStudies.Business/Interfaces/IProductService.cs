using LStudies.Business.Models;
using System;
using System.Threading.Tasks;

namespace LStudies.Business.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Guid id);
    }
}
