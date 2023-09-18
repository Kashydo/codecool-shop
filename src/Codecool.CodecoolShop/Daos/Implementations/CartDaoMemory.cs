using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class CartDaoMemory : ICart
    {
        private List<Cart> data = new List<Cart>();
        private static CartDaoMemory instance = null;

        private CartDaoMemory()
        {
        }

        public static CartDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new CartDaoMemory();
            }

            return instance;
        }
        public void Add(Cart item)
        {
            item.Id = data.Count + 1;
            data.Add(item);
        }

        public Cart Get(int id)
        {
            return data.Find(x => x.Id == id);
        }

        public IEnumerable<Cart> GetAll()
        {
            return data;
        }

        public void Remove(int id)
        {
            data.Remove(this.Get(id));
        }
    }
}
