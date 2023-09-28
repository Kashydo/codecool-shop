using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class Order : BaseModel
    {
        public Client Client { get; set; }
        public List<Item> Items { get; set; }
    }
}
