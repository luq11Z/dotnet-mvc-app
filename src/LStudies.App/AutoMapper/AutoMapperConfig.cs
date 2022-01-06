using AutoMapper;
using LStudies.App.ViewModels;
using LStudies.Business.Models;

namespace LStudies.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            /* Use reverse map only if there are no constructors with 
             * paramters in the business model entity. 
             * If there are, another config shoud be placed. */
            CreateMap<Provider, ProviderViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
        }
    }
}
