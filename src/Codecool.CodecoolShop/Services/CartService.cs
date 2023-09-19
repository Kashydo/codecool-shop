using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class CartService
    {

        private readonly ICartDao cartDao;


        public CartService(ICartDao cartDao)
        {
            this.cartDao = cartDao;

        }

        public Cart GetCart(int id)
        {
            return this.cartDao.Get(id);
        }

    }
}
