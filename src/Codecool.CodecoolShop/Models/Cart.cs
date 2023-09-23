using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;


namespace Codecool.CodecoolShop.Models
{

    public class Cart : BaseModel
    {
        const int BaseQuantity = 1;
        public int UserId { get; set; }
        public Dictionary<Product, int> ItemsInCart { get; set; }

        public decimal CartValue { get; set; }

        public Cart()
        {
            ItemsInCart = new Dictionary<Product, int>();
        }
        public void CalculateCartValue()
        {
            if (ItemsInCart == null)
            {
                CartValue = 0;
            }
            else
            {
                decimal sum = 0;
                foreach (var item in ItemsInCart)
                {
                    sum += item.Key.DefaultPrice * item.Value;
                }
                CartValue = sum;
            }
        }

        public void AddItemToCart(Product product)
        {
            try

            {
                if (product == null)
                {
                    Console.WriteLine("Product is null");
                }
                else
                {
                    if (ItemsInCart == null)
                    {
                        ItemsInCart = new Dictionary<Product, int>();
                    }

                    if (ItemsInCart.ContainsKey(product))
                    {
                        ItemsInCart[product] += BaseQuantity;
                    }
                    else
                    {
                        ItemsInCart.Add(product, BaseQuantity);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                // Możesz rzucić wyjątek lub zalogować go, ale warto zastanowić się, co zrobić w przypadku błędu
            }
        }
        public void AddItemToCart(Product product, int quantity)
        {
            try
            {
                int Hits = 0;
                foreach (var item in ItemsInCart)
                {
                    if (item.Key == product)
                    {
                        ItemsInCart[item.Key] = item.Value + quantity;
                        Hits++;
                    }
                }
                if (ItemsInCart == null || Hits == 0)
                {
                    ItemsInCart.Add(product, quantity);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
        }

        public void RemoveItemFromCart(Product product)
        {
            try
            {

                foreach (var item in ItemsInCart)
                {
                    if (item.Key == product)
                    {
                        ItemsInCart[item.Key] = item.Value + BaseQuantity;

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }

        }
        public void RemoveItemFromCart(Product product, int quantity)
        {
            try
            {

                foreach (var item in ItemsInCart)
                {
                    if (item.Key == product)
                    {
                        ItemsInCart[item.Key] = item.Value - quantity;

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }

        }

        public override string ToString()

        {
            if (ItemsInCart == null)
            {
                return "Cart is empty";
            }
            return "Items in Cart: " + ItemsInCart.Count;
        }


    }
}
