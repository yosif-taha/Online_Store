using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Domain.Exeptions.NotFound
{
    public class ProductNotFoundException(int id) : NotFoundException($"produce with id :{id} was not found")
    {
    }
}
