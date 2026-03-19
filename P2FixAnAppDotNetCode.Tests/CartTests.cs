using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Repositories;
using P2FixAnAppDotNetCode.Models.Services;
using Xunit;

namespace P2FixAnAppDotNetCode.Tests
{
    /// <summary>
    /// The Cart test class
    /// </summary>
    public class CartTests
    {
        [Fact]
        public void AddItemInCart()
        {
            Cart cart = new Cart();
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);

            /// <summary>
            /// Use of reflection Method to access, modify and inject a private element into test functions.
            /// The app logic is then not modified.
            /// Use of a var to retrieve the private list _products
            /// </summary>
            var accessList = typeof(ProductRepository).GetField("_products", BindingFlags.NonPublic | BindingFlags.Static);

            /// <summary>
            /// Create a new var which will store the private _products list after products generation
            /// Then this var is used into the test.
            /// </summary>
            var ProductsList = (List<Product>)accessList.GetValue(productRepository);

            var product1 = new Product(1, 10, 20, "name", "description");
            var product2 = new Product(1, 10, 20, "name", "description");

            /// <summary>
            /// Then this var is used into the test.
            /// And is then reinjected as _products (reflection) into the other functions used into the test : FindProductInCartLines()
            /// </summary>
            ProductsList.Add(product1);
            ProductsList.Add(product2);


            /// <summary>
            /// Here implicitely : _products = ProductsList
            /// </summary>
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);

            Assert.NotEmpty(cart.Lines);
            Assert.Single(cart.Lines);
            Assert.Equal(2, cart.Lines.First().Quantity);
        }

        [Fact]
        public void GetAverageValue()
        {
            ICart cart = new Cart();
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);

            IEnumerable<Product> products = productService.GetAllProducts();
            cart.AddItem(products.First(p => p.Id == 2), 2);
            cart.AddItem(products.First(p => p.Id == 5), 1);
            double averageValue = cart.GetAverageValue();
            double expectedValue = ((9.99 * 2) + 895.00) / 3;

            Assert.Equal(expectedValue, averageValue);
        }

        [Fact]
        public void GetTotalValue()
        {
            ICart cart = new Cart();
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);

            IEnumerable<Product> products = productService.GetAllProducts();
            cart.AddItem(products.First(p => p.Id == 1), 1);
            cart.AddItem(products.First(p => p.Id == 4), 3);
            cart.AddItem(products.First(p => p.Id == 5), 1);
            double totalValue = cart.GetTotalValue();
            double expectedValue = 92.50 + 32.50 * 3 + 895.00;

            Assert.Equal(expectedValue, totalValue);
        }

        [Fact]
        public void FindProductInCartLines()
        {
            Cart cart = new Cart();
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);

            IEnumerable<Product> products = productService.GetAllProducts();
            

            /// <summary>
            /// Use of reflection Method to access, modify and inject a private element into test functions.
            /// The app logic is then not modified.
            /// Use of a var to retrieve the private list _products
            /// </summary>
            var accessList = typeof(ProductRepository).GetField("_products",BindingFlags.NonPublic | BindingFlags.Static);

            /// <summary>
            /// Create a new var which will store the private _products list after products generation
            /// Then this var is used into the test.
            /// </summary>
            var ProductsList = (List<Product>)accessList.GetValue(productRepository);

            int id = 999;
            var testproduct = new Product(id, 10, 20, "name", "description");

            /// <summary>
            /// Then this var is used into the test.
            /// And is then reinjected as _products (reflection) into the other functions used into the test : FindProductInCartLines()
            /// </summary>
            ProductsList.Add(testproduct);


            /// <summary>
            /// Here implicitely : _products = ProductsList
            /// </summary>
            cart.AddItem(testproduct, 1);
            var result = cart.FindProductInCartLines(999);

            Assert.NotNull(result);
        }
    }
}
