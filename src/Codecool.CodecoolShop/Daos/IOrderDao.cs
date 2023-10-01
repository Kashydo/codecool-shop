using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IOrderDao : IDao<Order>
    {
        IEnumerable<Order> GetBy(Client client);

        decimal Total(Order order);
    }
}
