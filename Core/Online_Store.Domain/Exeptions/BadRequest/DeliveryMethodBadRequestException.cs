using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Domain.Exeptions.BadRequest
{
    public class DeliveryMethodBadRequestException() : BadRequestException("Delivery Method Bad Request !!")
    {
    }
}
