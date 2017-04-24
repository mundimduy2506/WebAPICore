using System;
using System.Collections.Generic;

namespace WebAPICore.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? EnrolledDate { get; set; }
    }
}
