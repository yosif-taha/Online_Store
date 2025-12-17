using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Shared
{
    public class JwtOptions
    {
        public string issuer { get; set; }
        public string auduance { get; set; }
        public string key { get; set; }
        public double durationindays { get; set; }
    }
}
