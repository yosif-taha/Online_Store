using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Domain.Exeptions.NotFound
{
    public class BasetNotFoungException(string id) : NotFoundException($"Basket with id: {id} is not found")
    {
    }
}
