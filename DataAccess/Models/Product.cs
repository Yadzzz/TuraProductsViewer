using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string DetailedDescription { get; set; } = null!;
    }
}
