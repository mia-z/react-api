using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAPI.API.Entities
{
    public class ByteImage
    {
        public int id { get; set; }
        public string Data { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
    }
}
