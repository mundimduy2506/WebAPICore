using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore.Models
{
    public class CustomerReport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? EnrolledDate { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }

    }
}
