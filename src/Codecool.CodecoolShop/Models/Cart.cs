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
        public Dictionary<Product, int> ItemsInCart {  get; set; }

        public decimal CartValue { get; set; }
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
                int Hits = 0;
                foreach (var item in ItemsInCart)
                {
                    if (item.Key == product)
                    {
                      
                        ItemsInCart[item.Key] = item.Value + BaseQuantity;
                        Hits++;
                    }
                }
                if (ItemsInCart == null || Hits == 0)
                {
                    ItemsInCart.Add(product, BaseQuantity);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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


    }
}
