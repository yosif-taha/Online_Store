using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Domain.Exeptions.BadRequest
{
    public class CreateOrUpdateBadRequestException() : 
        BadRequestException("Invalid Operation When Create Or Update Basket")
    {
    }
}
