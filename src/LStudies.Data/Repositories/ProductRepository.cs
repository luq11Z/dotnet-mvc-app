using LStudies.Business.Interfaces;
using LStudies.Business.Models;
using LStudies.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LStudies.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(LStudiesDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Product> GetProductProvider(Guid id)
        {
            return await dbContext.Products.AsNoTracking().Include(prd => prd.Provider).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsByProvider(Guid providerId)
        {
            return await Find(p => p.ProviderId == providerId);
        }

        public async Task<IEnumerable<Product>> GetProductsProviders()
        {
            return await dbContext.Products.AsNoTracking().Include(p => p.Provider).OrderBy(prd => prd.Name).ToListAsync();
        }
    }
}
