using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples.EntityClasses
{
    public class ProductStats
    {
        public ProductStats()
        {
            Max = Decimal.MinValue;
            Min = Decimal.MaxValue;
            TotalProducts = 0;
            Total = 0;
        }
        public int TotalProducts { get; set; }
        public decimal Total { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public decimal Average { get; set; }

        public ProductStats Accumulate(Product prod)
        {
            TotalProducts += 1;
            Total += prod.ListPrice;
            Max = Math.Max(Max,prod.ListPrice);
            Min = Math.Min(Min, prod.ListPrice);
            return this;
        }

        public ProductStats ComputeAverage()
        {
            Average = Total / TotalProducts;
            return this;
        }

    }
}
