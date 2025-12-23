namespace Online_Store.Domain.Entites.Orders
{
    //table
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductInItemOrderd product, int quentity, decimal price)
        {
            Product = product;
            Quentity = quentity;
            Price = price;
        }

        public ProductInItemOrderd Product { get; set; }
        public int Quentity { get; set; }
        public decimal Price { get; set; }
    }
}