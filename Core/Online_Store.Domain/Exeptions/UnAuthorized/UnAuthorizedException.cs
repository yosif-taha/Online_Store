using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Domain.Exeptions.UnAuthorized
{
    public class UnAuthorizedException() : Exception("You Are Not Authorized !!")
    {
    }
}
