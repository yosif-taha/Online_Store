using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Domain.Exeptions.NotFound
{
    public class DeliveryMethodNotFoundException(int id) : NotFoundException($"Delivery Method with Id {id} was not found !!")
    {
    }
}
