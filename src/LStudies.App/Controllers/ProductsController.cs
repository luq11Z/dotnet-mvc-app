using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LStudies.App.ViewModels;
using LStudies.Business.Interfaces;
using AutoMapper;
using LStudies.Business.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using LStudies.App.Extensions;

namespace LStudies.App.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IProductService _productService;
        private IMapper _mapper;

        public ProductsController(IProductRepository productRepository, 
                                  IProviderRepository providerRepository,
                                  IProductService productService,
                                  IMapper mapper,
                                  INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
            _providerRepository = providerRepository;
            _productService = productService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("products-list")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsProviders()));
        }

        [AllowAnonymous]
        [Route("product-details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [ClaimsAuthorize("Products", "Create")]
        [Route("new-product")]
        public async Task<IActionResult> Create()
        {
            var productViewModel = await PopulateProviders(new ProductViewModel());

            return View(productViewModel);
        }

        [ClaimsAuthorize("Products", "Create")]
        [Route("new-product")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await PopulateProviders(productViewModel);

            if (!ModelState.IsValid)
            {
                return View(productViewModel);
            }

            var imgPrefix = Guid.NewGuid() + "_";

            if (!await UploadImage(productViewModel.ImageUpload, imgPrefix))
            {
                return View(productViewModel);
            }

            productViewModel.Image = imgPrefix + productViewModel.ImageUpload.FileName;
            await _productService.Add(_mapper.Map<Product>(productViewModel));

            if (!IsOperationValid())
            {
                return View(productViewModel);
            }

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Products", "Edit")]
        [Route("edit-product/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [ClaimsAuthorize("Products", "Edit")]
        [Route("edit-product/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }

            var productViewModelUpdate = await GetProduct(id);
            productViewModel.Provider = productViewModelUpdate.Provider;
            productViewModel.Image = productViewModelUpdate.Image;

            if (!ModelState.IsValid)
            {
                return View(productViewModel);
            }

            if (productViewModel.ImageUpload != null)
            {
                var imgPrefix = Guid.NewGuid() + "_";
                if (!await UploadImage(productViewModel.ImageUpload, imgPrefix))
                {
                    return View(productViewModel);
                }

                productViewModelUpdate.Image = imgPrefix + productViewModel.ImageUpload.FileName;
            }

            productViewModelUpdate.Name = productViewModel.Name;
            productViewModelUpdate.Description = productViewModel.Description;
            productViewModelUpdate.Price = productViewModel.Price;
            productViewModelUpdate.IsActive = productViewModel.IsActive;

            await _productService.Update(_mapper.Map<Product>(productViewModelUpdate));

            if (!IsOperationValid())
            {
                return View(productViewModel);
            }

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Products", "Delete")]
        [Route("delete-product/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [ClaimsAuthorize("Products", "Delete")]
        [Route("delete-product/{id:guid}")]
        [HttpPost, ActionName("Delete")] // ActionName makes the method responds to Delete Action
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var prodcutViewModel = await GetProduct(id);

            if (prodcutViewModel == null)
            {
                return NotFound();
            }

            await _productService.Delete(id);

            if (!IsOperationValid())
            {
                return View(prodcutViewModel); // In case of failure the it will return the user to the Delete View (because of the ActionName("Delete"))
            }

            TempData["Success"] = "Product successfully deleted";

            return RedirectToAction("Index");
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.GetProductProvider(id));
            product.Providers = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());

            return product;
        }

        private async Task<ProductViewModel> PopulateProviders(ProductViewModel product)
        {
            product.Providers = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());

            return product;
        }

        private async Task<bool> UploadImage(IFormFile file, string imgPrefix)
        {
            if (file.Length <= 0)
            {
                return false;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefix + file.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, file.FileName + " already existis!");
                return false;
            }

            var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return true;
        }
    }
}
