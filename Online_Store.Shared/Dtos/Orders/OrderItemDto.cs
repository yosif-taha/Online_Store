using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Shared.Dtos.Orders
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string productName { get; set; }
        public string PictrulUrl { get; set; }
        public decimal Cost { get; set; }
        public int Quantety { get; set; }
    }
}
