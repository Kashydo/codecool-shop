using Codecool.CodecoolShop.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class OrderDaoMemory : IOrderDao
    {
        private List<Order> data = new List<Order>();
        private static OrderDaoMemory instance = null;
        string orderPath = "~\\SavedOrders\\Orders.css";
        string clientPath = "~\\SavedOrders\\Clients.css";
        string separator = ",";
        string[] headings = { "Order Id", "Client Id", "Item Id", "Quantity", "Value" };

        public OrderDaoMemory()
        {
            if (!System.IO.File.Exists(orderPath))
            {
                using (var fileStream = System.IO.File.CreateText(orderPath))
                {
                    fileStream.WriteLine(string.Join(separator, headings));
                }
            }
        }

        public static OrderDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new OrderDaoMemory();
            }

            return instance;
        }


        public void Add(Order order)
        {
            foreach (var item in order.Items)
            {
                string[] newLine = {
                    order.Id.ToString(),
                    order.Client.Id.ToString(),
                    item.Product.Id.ToString(),
                    item.Quantity.ToString(),
                    item.Value().ToString("C2")
                };
                string newLineText = string.Join(separator, newLine);
                using (StreamWriter sw = File.AppendText(orderPath))
                {
                    sw.WriteLine(newLineText);
                }
            }
        }

        public Order Get(int id)
        {
            var order = new Order();
            List<Item> items = new List<Item>();

            string[] lines = File.ReadAllLines(orderPath);
            foreach (string line in lines.Skip(1))
            {
                string[] parts = line.Split(separator);
                if (parts.Length == headings.Length)
                {

                    var clientData = new ClientDaoMemory();
                    var ProductData = new ProductDaoMemory();


                    int orderId = int.Parse(parts[0]);
                    if (orderId == id)
                    {
                        if (order.Id == 0)
                        {
                            order.Id = orderId;
                            order.Client = clientData.Get(int.Parse(parts[1]));
                        }
                        Product productInCart = ProductData.Get(int.Parse(parts[2]));
                        var item = new Item();
                        item.Product = productInCart;
                        item.Quantity = int.Parse(parts[3]);
                        items.Add(item);
                    }
                }
            }
            order.Items = items;
            return order;

        }



        public IEnumerable<Order> GetAll()
        {
            List<int> orderIDs = new List<int>();
            string[] lines = File.ReadAllLines(orderPath);
            foreach (string line in lines.Skip(1))
            {
                string[] parts = line.Split(separator);
                if (parts.Length == headings.Length)
                {
                    if (!orderIDs.Contains(int.Parse(parts[0])))
                    {
                        var order = Get(int.Parse(parts[0]));
                        orderIDs.Add(order.Id);
                        data.Add(order);
                    }
                }
            }
            return data;
        }

        public IEnumerable<Order> GetBy(Client client)
        {
            List<Order> orders = GetAll().ToList();
            List<Order> ordersFromClint = new List<Order>();
            foreach (var order in orders)
            {
                if (order.Client.Id == client.Id)
                {
                    ordersFromClint.Add(order);
                }
            }
            return ordersFromClint;
        }

        public void Remove(int id)
        {
            string[] lines = File.ReadAllLines(orderPath);


            List<string> updatedLines = new List<string>();

            foreach (string line in lines)
            {
                string[] parts = line.Split(separator);
                if (parts.Length == headings.Length)
                {
                    int orderId = int.Parse(parts[0]);
                    if (orderId != id)
                    {
                        updatedLines.Add(line);
                    }
                }
            }


            File.WriteAllLines(orderPath, updatedLines);
        }

        public decimal Total(Order order)
        {
            List<Order> orders = GetAll().ToList();
            decimal total = 0;
            foreach (var o in orders)
            {
                if (o.Id == order.Id)
                {
                    foreach (var item in o.Items)
                    {
                        total += item.Value();
                    }
                }
            }
            return total;
        }
    }
}
