using InlämningsUppgift2.Entities;
using InlämningsUppgift2.Models;
using InlämningsUppgift2.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2.Services
{
    internal class ProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly MoneyPaymentRepository _moneyPaymentRepository;
        private readonly ProductCategoryRepository _productCategoryRepository;

        public ProductService(ProductRepository productRepository, MoneyPaymentRepository moneyPaymentRepository, ProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _moneyPaymentRepository = moneyPaymentRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<bool> CreateProductAsync(ProductRegistrationForm form)
        {
            if (!await _productRepository.ExistsAsync(x => x.ProductName == form.ProductName))
            {

                
                
                // check if Money Payment exist else create
                var moneyPaymentEntity = await _moneyPaymentRepository.GetAsync(x => x.Payment == form.MoneyPayment);
                moneyPaymentEntity ??= await _moneyPaymentRepository.CreateAsync(new MoneyPaymentEntity { Payment = form.MoneyPayment });

                //check if category exist else create
                var productCategoryEntity = await _productCategoryRepository.GetAsync(x => x.CategoryName == form.ProductCategory);
                productCategoryEntity ??= await _productCategoryRepository.CreateAsync(new ProductCategoryEntity { CategoryName = form.ProductCategory });

                //create product

                var productEntity = await _productRepository.CreateAsync(new ProductEntity { ProductName = form.ProductName, ProductDescription = form.ProductDescription, ProductPrice = form.Price, MoneyPaymentId = moneyPaymentEntity.Id, ProductCategoryId = productCategoryEntity.Id });
                if (productEntity != null)
                    return true;
            } 
            return false;
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products;
        }

        /*  public async Task<bool> UpdateAsync(ProductEntity updatedProduct)
          {

          }*/

        public async Task<bool> DeleteProductAsync(int productId)
        {
            try
            {
                var product = await _productRepository.GetAsync(x => x.Id == productId);
                if (product != null)
                    return await _productRepository.DeleteAsync(product);

                else
                    return false;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return false;

        }

        public async Task<bool> UpdateProductAsync(int productId, ProductRegistrationForm form)
        {
            var existingProduct = await _productRepository.GetAsync(productId);

            if (existingProduct != null)
            {
                existingProduct.ProductName = form.ProductName;
                existingProduct.ProductDescription = form.ProductDescription;
                existingProduct.ProductPrice = form.Price;
                existingProduct.MoneyPayment.Payment = form.MoneyPayment;
                existingProduct.ProductCategory.CategoryName = form.ProductCategory;

                return await _productRepository.UpdateAsync(existingProduct);
            }

            return false;
        }

        public async Task<ProductEntity> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetAsync(productId);
        }

    }
}
