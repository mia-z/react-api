using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAPI.API.Entities
{
    public class Tag
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
    }
}
