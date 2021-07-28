using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples.EntityClasses
{
    public class ProductSales
    {
        public Product Product { get; set; }
        public IEnumerable<SalesOrderDetail> Sales { get; set; }
    }
}
