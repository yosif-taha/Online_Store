using System.Text.Json.Serialization;

namespace Online_Store.Shared.Dtos.Baskets
{
    public class BasketItemsDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("productName")]
        public string ProductName { get; set; }
        [JsonPropertyName(name: "pictureUrl")]
        public string PictureUrl { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}