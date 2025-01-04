using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.furnitureStore.shared
{
    public class OrderDetail
    {
        public int Idorder { get; set; }
        public int IdProduct { get; set; }
        public int quantity { get; set; }
    }
}
