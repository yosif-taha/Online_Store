using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Domain.Exeptions.NotFound
{
    public class OrderNotFoundException(Guid id) : NotFoundException($"Order With Id : {id} Was Not Found")
    {
    }
}
