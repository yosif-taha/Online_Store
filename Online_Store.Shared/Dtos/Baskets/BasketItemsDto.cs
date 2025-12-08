using System.Text.Json.Serialization;

namespace Online_Store.Shared.Dtos.Baskets
{
    public class BasketItemsDto
    {
        public int Id { get; set; }
        [JsonPropertyName("productName")]
        public string ProductName { get; set; }
        [JsonPropertyName(name: "pictureUrl")]
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}