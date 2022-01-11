using LStudies.Business.Interfaces;
using LStudies.Business.Models;
using LStudies.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LStudies.Business.Services
{
    public class ProviderService : BaseService, IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAddressRepository _addressRepository;

        public ProviderService(IProviderRepository providerRepository, IAddressRepository addressRepository, INotifier notifier) : base(notifier)
        {
            _providerRepository = providerRepository;
            _addressRepository = addressRepository;
        }

        public async Task Add(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider) || !ExecuteValidation(new AddressValidation(), provider.Address))
            {
                return;
            }

            if (_providerRepository.Find(p => p.Document == provider.Document).Result.Any())
            {
                Notify("There is already a provider with the given Document.");
                
                return;
            }

            await _providerRepository.Add(provider);

            return;
        }

        public async Task Update(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider))
            {
                return;
            }

            if (_providerRepository.Find(p => p.Document == provider.Document && p.Id != provider.Id).Result.Any())
            {
                Notify("There is already a provider with the given Document.");

                return;
            }

            await _providerRepository.Update(provider);
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address))
            {
                return;
            }

            await _addressRepository.Update(address);
        }

        public async Task Delete(Guid id)
        {
            if (_providerRepository.GetProviderProductsAdress(id).Result.Products.Any())
            {
                Notify("Provider has registered produts!");

                return;
            }

            await _providerRepository.Delete(id);
        }

        public void Dispose()
        {
            _providerRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}
