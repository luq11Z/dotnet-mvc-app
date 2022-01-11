using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LStudies.App.ViewModels;
using LStudies.Business.Interfaces;
using AutoMapper;
using LStudies.Business.Models;
using Microsoft.AspNetCore.Authorization;
using LStudies.App.Extensions;

namespace LStudies.App.Controllers
{
    [Authorize]
    public class ProvidersController : BaseController
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;

        public ProvidersController(IProviderRepository providerRepository, 
                                   IMapper mapper,
                                   IProviderService providerService,
                                   INotifier notifier) : base(notifier)
        {
            _providerRepository = providerRepository;
            _mapper = mapper;
            _providerService = providerService;
        }

        [AllowAnonymous]
        [Route("providers-list")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll()));
        }

        [AllowAnonymous]
        [Route("provider-details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);

            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

        [ClaimsAuthorize("Providers", "Create")]
        [Route("new-provider")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Providers", "Create")]
        [Route("new-provider")]
        [HttpPost]
        public async Task<IActionResult> Create(ProviderViewModel providerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(providerViewModel);
            }

            var provider = _mapper.Map<Provider>(providerViewModel);
            await _providerService.Add(provider);

            if (!IsOperationValid())
            {
                return View(providerViewModel);
            }

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Providers", "Edit")]
        [Route("edit-provider/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var providerViewModel = await GetProviderProductsAddress(id);

            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

        [ClaimsAuthorize("Providers", "Edit")]
        [Route("edit-provider/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProviderViewModel providerViewModel)
        {
            if (id != providerViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(providerViewModel);
            }

            var provider = _mapper.Map<Provider>(providerViewModel);
            await _providerService.Update(provider);

            if (!IsOperationValid())
            {
                providerViewModel = await GetProviderProductsAddress(id);

                return View(providerViewModel);
            }

            return RedirectToAction("Index");
            
        }

        [ClaimsAuthorize("Providers", "Delete")]
        [Route("delete-provider/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);

            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

        [ClaimsAuthorize("Providers", "Delete")]
        [Route("delete-provider/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);
            
            if(providerViewModel == null)
            {
                return NotFound();
            }

            await _providerService.Delete(id);

            if (!IsOperationValid())
            {
                return View(providerViewModel);
            }

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [Route("get-provider-address/{id:guid}")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);

            if (providerViewModel == null)
            {
                return NotFound();
            }

            return PartialView("_AddressDetails", providerViewModel);
        }


        /* Instatiate entity by searching with it's id and return as a View Model (DTO) */
        [ClaimsAuthorize("Providers", "Edit")]
        [Route("update-provider-address/{id:guid}")]
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var provider = await GetProviderAddress(id);

            if(provider == null)
            {
                return NotFound();
            }

            /* Identify the partial view we want to render with entity data */
            return PartialView("_UpdateAddress", new ProviderViewModel { Address = provider.Address });
        }

        [ClaimsAuthorize("Providers", "Edit")]
        [Route("update-provider-address/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> UpdateAddress(ProviderViewModel providerViewModel)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document");

            if (!ModelState.IsValid)
            {
                return PartialView("_UpdateAddress", providerViewModel);
            }

            await _providerService.UpdateAddress(_mapper.Map<Address>(providerViewModel.Address));

            if (!IsOperationValid())
            {
                return View(providerViewModel);
            }

            var url = Url.Action("GetAddress", "Providers", new { id = providerViewModel.Address.ProviderId });

            return Json(new { success = true, url });
        }

        /* This method is called a few times within the controller, that's why it is created here*/
        private async Task<ProviderViewModel> GetProviderAddress(Guid id)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderAdress(id));
        }

        /* This method is called a few times within the controller, that's why it is created here*/
        private async Task<ProviderViewModel> GetProviderProductsAddress(Guid id)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderProductsAdress(id));
        }
    }
}
