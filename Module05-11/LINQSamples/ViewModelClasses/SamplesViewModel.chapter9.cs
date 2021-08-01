using LINQSamples.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples.ViewModelClasses
{
    /// <summary>
    /// chapter 9
    /// </summary>
    public partial class SamplesViewModel
    {
        public void GroupBy()
        {
            IEnumerable<IGrouping<string, Product>> sizeGroup;
            sizeGroup = (from prod in Products
                         orderby prod.Size
                         group prod by prod.Size).ToList();

            sizeGroup = Products.OrderBy(p=>p.Size)
                .GroupBy(p=>p.Size);

            foreach(var group in sizeGroup)
            {
                Console.WriteLine($"Size: {group.Key} Count: {group.Count()}");
                foreach(var prod in group)
                {
                    Console.WriteLine($"ID: {prod.ProductID} Size: {prod.Size}");
                }
            }
        }
        public void GroupByInto()
        {
            IEnumerable<IGrouping<string, Product>> sizeGroup;
            sizeGroup = (from prod in Products
                         orderby prod.Size
                         group prod by prod.Size into sizes
                         select sizes).ToList();

            sizeGroup = (from prod in Products
                         group prod by prod.Size into sizes
                         orderby sizes.Key
                         select sizes).ToList();

            sizeGroup = Products.OrderBy(p => p.Size)
                .GroupBy(p => p.Size);

            foreach (var group in sizeGroup)
            {
                Console.WriteLine($"Size: {group.Key} Count: {group.Count()}");
                foreach (var prod in group)
                {
                    Console.WriteLine($"ID: {prod.ProductID} Size: {prod.Size}");
                }
            }
        }

        public void GroupByHaving()
        {
            IEnumerable<IGrouping<string, Product>> sizeGroup;
            sizeGroup = (from prod in Products
                         orderby prod.Size
                         group prod by prod.Size into sizes
                         where sizes.Count()>2
                         select sizes).ToList();
        }

        public void OnetoMany()
        {
            IEnumerable<IGrouping<string, Product>> sizeGroup;
            sizeGroup = (from prod in Products
                         orderby prod.Size
                         group prod by prod.Size into sizes
                         where sizes.Count() > 2
                         select sizes).ToList();
        }

        public void GroupedSubquery()
        {
            var saleg = (from sale in Sales
                              group sale by sale.ProductID
                              into sales
                              select sales).ToList();

            var salesGroup = (from sale in Sales
                              group sale by sale.ProductID
                              into sales
                              select new SaleProducts
                              {
                                  SalesOrderID = sales.Key,
                                  Products = (from prod in Products
                                              join sale in sales
                                              on prod.ProductID equals sale.ProductID
                                              where sale.SalesOrderID == sales.Key
                                              select prod).ToList()
                              }).ToList();  
        }

    }
}
