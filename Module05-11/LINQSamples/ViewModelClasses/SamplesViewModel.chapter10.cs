using LINQSamples.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples.ViewModelClasses
{
    public partial class SamplesViewModel
    {
        public void CountMinMaxfiltered()
        {
            string search = "Red";

            var value = (from prod in Products
                         select prod
                        ).Count(prod => prod.Color == search);

            var value1 = (from prod in Products
                     select prod.ListPrice).Min();

            var value2 = (from prod in Products
                          select prod.ListPrice).Max();

            var value3 = (from prod in Products
                          select prod
                        ).Aggregate(0M, (sum,prod) => sum += prod.ListPrice);

            var value4 = (from sale in Sales
                          select sale
            ).Aggregate(0M, (sum, sale) => sum += sale.OrderQty * sale.UnitPrice);
        }

        public void AggregateUsingGrouping()
        {
            var stats = (from prod in Products
                         group prod by prod.Size into sizeGroup
                         where sizeGroup.Count() > 0
                         select new
                         {
                             Size = sizeGroup.Key,
                             TotalProducts = sizeGroup.Count(),
                             Max = sizeGroup.Max(s=>s.ListPrice),
                             Min = sizeGroup.Min(s => s.ListPrice),
                             Average = sizeGroup.Average(s=>s.ListPrice)
                         }
                         into result orderby result.Size
                         select result).ToList();
        }

        public void AggregateUsingGroupingMoreEfficient()
        {
            var stats = Products.GroupBy(sale => sale.Size)
                .Where(sizeGroup => sizeGroup.Count() > 0)
                .Select(sizeGroup =>
                {
                    var result = sizeGroup.Aggregate(new ProductStats(),
                        (acc, prod) => acc.Accumulate(prod),
                        acc => acc.ComputeAverage());
                    return new
                    {
                        Size = sizeGroup.Key,
                        result.TotalProducts,
                        result.Min,
                        result.Max,
                        result.Average
                    };
                })
                .OrderBy(result => result.Size)
                .Select(result => result).ToList();
        }

    }
}
