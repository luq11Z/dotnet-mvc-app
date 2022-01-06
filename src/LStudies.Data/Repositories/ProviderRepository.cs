using LStudies.Business.Interfaces;
using LStudies.Business.Models;
using LStudies.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LStudies.Data.Repositories
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(LStudiesDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Provider> GetProviderAdress(Guid id)
        {
            return await dbContext.Providers.AsNoTracking().Include(p => p.Address).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Provider> GetProviderProductsAdress(Guid id)
        {
            return await dbContext.Providers.AsNoTracking()
                .Include(p => p.Products)
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
