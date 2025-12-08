namespace Online_Store.Domain.Entites.Basket
{
    public class BasketItems
    {
        public int Id { get; set; }
        public string PrpductName { get; set; }
        public string PictrulUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}