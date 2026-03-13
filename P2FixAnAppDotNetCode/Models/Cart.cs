//using Microsoft.AspNetCore.Mvc;
//using System;
using P2FixAnAppDotNetCode.Models.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => _cartLines.AsReadOnly();

        //Create a new CartLine
        private List<CartLine> _cartLines = new List<CartLine>();

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        public List<CartLine> GetCartLineList()
        {
            return _cartLines;
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            // TODO implement the method
            int quantityToAdd = 1;
            int i = 0;
            var cartLine = FindProductInCartLines(product.Id);

            //Returns nothing if the product doesn't exist or if the stock quantity is null
            if (product == null || product.Stock == 0)
            {
                return;
            }

            //If the item is already into the cart, add 1 to the quantity
            if (cartLine != null)
            {
                if (product.Stock < (quantity + cartLine.Quantity))
                {
                    cartLine.Quantity += product.Stock;
                }
                else
                {
                    quantityToAdd = quantity; //Mod for automated tests : allows to add more than 1 product in one step (click)
                    cartLine.Quantity += quantityToAdd;
                }
                return;
            }
            else
            {
                if (product.Stock < quantity)
                {
                    quantityToAdd = product.Stock;
                }
                quantityToAdd = quantity; //Mod for automated tests : allows to add more than 1 product in one step (click)
                AddLine(new CartLine(i++, product, quantityToAdd));
                return;
            }
        }

        /// <summary>
        /// Adds a product into the cart
        /// </summary>
        public void AddLine(CartLine cartLine) =>
            GetCartLineList().Add(cartLine);

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product)
        {
            var cartLine = FindProductInCartLines(product.Id);

            if (cartLine.Quantity > 1)
            {
                cartLine.Quantity -= 1;
            }
            else
            {
                GetCartLineList().Remove(cartLine);
            }
        }

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            // TODO implement the method
            double totalCartValue = 0;

            foreach (var cartLine in GetCartLineList())
            {
                totalCartValue += (cartLine.Product.Price * cartLine.Quantity);
            }

            return totalCartValue;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            // TODO implement the method
            int i = 0;

            foreach (var cartLine in GetCartLineList())
            {
                i += cartLine.Quantity;
            }
            //To avoid impossile division x/0 => x/1
            if (i == 0)
            {
                i = 1;
            }
            return GetTotalValue() / i;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public CartLine FindProductInCartLines(int productId)
        {
            // TODO implement the method

            // Retrieve the complete cartlist
            var cartLines = GetCartLineList();

            // Retrieve only the cart line which matches the selected product
            foreach (var cartLine in cartLines)
            {
                if (cartLine.Product.Id == productId)
                {
                    return cartLine;
                }
            }
            return null;
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }
        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            //IList<CartLine> cartLines = GetCartLineList();
            //cartLines.Clear();
            IList<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {

        public CartLine(int orderLineId, Product product, int quantity)
        {
            OrderLineId = orderLineId;
            Product = product;
            Quantity = quantity;
        }
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}