using P2FixAnAppDotNetCode.Controllers;
using P2FixAnAppDotNetCode.Models.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Security.Cryptography.X509Certificates;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// This class provides services to manages the products
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all product from the inventory
        /// </summary>
        public Product[] GetAllProducts()
        {
            // TODO change the return type from array to List<T> and propagate the change
            // throughout the application
            // Get the array from ProductRepository.cs
            Product[] products = _productRepository.GetAllProducts();
            // Convert to a list and propagate
            // Old version products.ToList();
            products.ToList<Product>();
            return products;
        }

        /// <summary>
        /// Get a product form the inventory by its id
        /// </summary>
        public Product GetProductById(int id)
        {
            // TODO implement the method
            Product[] products = GetAllProducts();

            foreach (Product product in products)
            {
                if (product.Id == id)
                {
                    return product;
                }
            }

            return null;
        }

        /// <summary>
        /// Update the quantities left for each product in the inventory depending of ordered the quantities
        /// </summary>
        public void UpdateProductQuantities(Cart cart)
        {
            // TODO implement the method
            // update product inventory by using _productRepository.UpdateProductStocks() method.
            foreach (var cartLine in cart.GetCartLineList())
            {
                int quantityToRemove = cartLine.Quantity;
                _productRepository.UpdateProductStocks(cartLine.Product.Id, quantityToRemove);
            }
        }
    }
}
