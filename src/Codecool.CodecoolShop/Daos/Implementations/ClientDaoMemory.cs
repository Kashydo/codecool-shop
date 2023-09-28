
using Codecool.CodecoolShop.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ClientDaoMemory : IClientDao
    {
        private List<Client> data = new List<Client>();
        private static ClientDaoMemory instance = null;
        string filePath = "~\\SavedOrders\\Clients.css";
        string separator = ",";
        string[] headings = { "ClientID", "First Name", "Last Name", "Country", "Region", "City", "Postal Code", "Streat", "House Number", "Flat Number", "PhoneNumber", "Email" };
        public ClientDaoMemory()
        {

            if (!System.IO.File.Exists(filePath))
            {
                using (var fileStream = System.IO.File.CreateText(filePath))
                {
                    fileStream.WriteLine(string.Join(separator, headings));
                }
            }

        }

        public static ClientDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new ClientDaoMemory();
            }

            return instance;
        }
        public void Add(Client client)
        {
            String[] newLine = {
                client.Id.ToString(),
                client.Name,
                client.Surname,
                client.Country,
                client.Region,
                client.City,
                client.PostalCode,
                client.Streat,
                client.HouseNumber,
                client.FlatNumber ?? "",
                client.PhoneNumber,
                client.Email };
            string newLineText = string.Join(separator, newLine);
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(newLineText);
            }
        }

        public Client Get(int id)
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines.Skip(1))
            {
                string[] parts = line.Split(separator);
                if (parts.Length == headings.Length)
                {
                    int clientId = int.Parse(parts[0]);
                    if (clientId == id)
                    {

                        var client = new Client
                        {
                            Id = clientId,
                            Name = parts[1],
                            Surname = parts[2],
                            Country = parts[3],
                            Region = parts[4],
                            City = parts[5],
                            PostalCode = parts[6],
                            Streat = parts[7],
                            HouseNumber = parts[8],
                            PhoneNumber = parts[10],

                        };
                        if (!string.IsNullOrEmpty(parts[9]))
                        {
                            client.FlatNumber = parts[9];
                        }
                        else
                        {
                            client.FlatNumber = null;
                        }
                        return client;
                    }
                }
            }
            return null;
        }

        public IEnumerable<Client> GetAll()
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines.Skip(1))
            {
                string[] parts = line.Split(separator);
                if (parts.Length == headings.Length)
                {
                    var client = new Client
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[1],
                        Surname = parts[2],
                        Country = parts[3],
                        Region = parts[4],
                        City = parts[5],
                        PostalCode = parts[6],
                        Streat = parts[7],
                        HouseNumber = parts[8],
                        PhoneNumber = parts[10],

                    };
                    if (!string.IsNullOrEmpty(parts[9]))
                    {
                        client.FlatNumber = parts[9];
                    }
                    else
                    {
                        client.FlatNumber = null;
                    }
                    data.Add(client);
                }
            }
            return data;
        }
        public void Remove(int id)
        {
            string[] lines = File.ReadAllLines(filePath);


            List<string> updatedLines = new List<string>();

            foreach (string line in lines)
            {
                string[] parts = line.Split(separator);
                if (parts.Length == headings.Length)
                {
                    int clientId = int.Parse(parts[0]);
                    if (clientId != id)
                    {
                        updatedLines.Add(line);
                    }
                }
            }


            File.WriteAllLines(filePath, updatedLines);
        }
    }
}
