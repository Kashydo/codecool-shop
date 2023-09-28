﻿using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IOrderDao : IDao<Order>
    {
        IEnumerable<Product> GetBy(Client client);

        int Total(Order order);
    }
}
