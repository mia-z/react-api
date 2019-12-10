using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAPI.API.Entities
{
    public class Brand
    {
        public int id { get; set; }
        public int ImageId { get; set; }
        public string Data { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
