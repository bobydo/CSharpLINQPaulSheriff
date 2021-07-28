using LINQSamples.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples.ViewModelClasses
{
    /// <summary>
    /// chapter 8 : Join 2 collections
    /// partial class has to use same namespacs
    /// https://stackoverflow.com/questions/4132984/access-class-fields-from-partial-class
    /// </summary>
    public partial class SamplesViewModel
    {
        public void InnerJoinWithTwoProperties()
        {
            var query = (from prod in Products
                         join sale in Sales
                         on prod.ProductID equals sale.ProductID
                         select new
                         {
                             prod.ProductID,
                             prod.Name,
                             sale.SalesOrderID,
                             sale.OrderQty,
                             sale.UnitPrice
                         }).ToList();

            short qty = 6;
            var query1 = (from prod in Products
                          join sale in Sales
                          on new { prod.ProductID, Qty = qty }
                          equals
                          new { sale.ProductID, Qty = sale.OrderQty }
                          select new
                          {
                              prod.ProductID,
                              prod.Name,
                              sale.SalesOrderID,
                              sale.OrderQty,
                              sale.UnitPrice
                          }).ToList();
        }
        public void GroupJoin()
        {
            var grouped = 
                (from prod in Products
                           join sale in Sales
                           on prod.ProductID equals sale.ProductID
                           into sales
                           select new ProductSales
                           {
                               Product = prod,
                               Sales = sales
                }).ToList();
        }
    }
}
