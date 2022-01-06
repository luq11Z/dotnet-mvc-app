using LStudies.Business.Interfaces;
using LStudies.Business.Models;
using LStudies.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LStudies.Data.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(LStudiesDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Address> GetAddressByProvider(Guid providerId)
        {
            return await dbContext.Addresses.AsNoTracking().FirstOrDefaultAsync(a => a.ProviderId == providerId);
        }
    }
}
