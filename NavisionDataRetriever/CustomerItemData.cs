using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavisionDataRetriever
{
    public class CustomerItemData
    {
        public string Id { get; set; }
        public string Price { get; set; }

        public CustomerItemData(string id, string price)
        {
            this.Id = id;
            this.Price = price;
        }
    }
}
