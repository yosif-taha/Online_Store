namespace Online_Store.Domain.Entites.Orders
{
    //part of Order table
    public class OrderAddress
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}