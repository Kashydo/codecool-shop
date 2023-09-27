namespace Codecool.CodecoolShop.Models
{
    public class Item : BaseModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal Value()
        {
            return Product.DefaultPrice * Quantity;
        }
    }
}
