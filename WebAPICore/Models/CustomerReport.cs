using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore.Models
{
    public class CustomerReport: Customer
    {
        public string Gender { get; set; }
        public string Country { get; set; }

    }
}
