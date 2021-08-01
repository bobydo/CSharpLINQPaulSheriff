using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples.EntityClasses
{
    public class SaleProducts
    {
        public int SalesOrderID { get; set; }
        public List<Product> Products { get; set; }
    }
}
