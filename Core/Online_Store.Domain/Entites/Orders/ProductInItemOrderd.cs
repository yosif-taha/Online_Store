namespace Online_Store.Domain.Entites.Orders
{
    // part of productItem table
    public class ProductInItemOrderd
    {
        public ProductInItemOrderd()
        {
            
        }
        public ProductInItemOrderd(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}