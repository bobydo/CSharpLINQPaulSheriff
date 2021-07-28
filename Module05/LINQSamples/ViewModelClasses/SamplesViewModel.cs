using LINQSamples.RepositoryClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQSamples
{
  public class SamplesViewModel
  {
    #region Constructor
    public SamplesViewModel()
    {
      // Load all Product Data
      Products = ProductRepository.GetAll();
      // Load all Sales Data
      Sales = SalesOrderDetailRepository.GetAll();
    }
    #endregion

    #region Properties
    public bool UseQuerySyntax { get; set; } = true;
    public List<Product> Products { get; set; }
    public List<SalesOrderDetail> Sales { get; set; }
    public string ResultText { get; set; }
    #endregion

    public void WhereExpression()
        {
            string search = "L";
            decimal cost = 100;
            if(UseQuerySyntax)
            {
                Products = (from prod in Products
                            where prod.Name.StartsWith(search)
                            && prod.StandardCost >cost
                            select prod).ToList();
            }
            else
            {
                Products = Products
                    .Where(p => p.Name.StartsWith(search)
                    && p.StandardCost > cost).ToList();
            }

        }

        public void WhereExtension()
        {
            string color = "Red";
            if (UseQuerySyntax)
            {
                Products = (from prod in Products
                            select prod).ByColor(color).ToList();
            }
            else
            {
                Products = Products.ByColor(color).ToList();
            }

        }

        public void WhereThrowException()
        {
            try { 
                string color = "Red";
                if (UseQuerySyntax)
                {
                    var product = (from prod in Products
                                select prod).ByColor(color).First();
                }
                else
                {
                    //more thna 2 throw exception
                    var product = Products.ByColor(color).SingleOrDefault();
                }
            }
            catch
            {
                //not found
            }
        }


        #region ForEach Method
        /// <summary>
        /// ForEach allows you to iterate over a collection to perform assignments within each object.
        /// In this sample, assign the Length of the Name property to a property called NameLength
        /// When using the Query syntax, assign the result to a temporary variable.
        /// </summary>
        public void ForEach()
        {
          if (UseQuerySyntax) {
                Products = (from prod in Products
                                //temporary variable and assgin value for tmp
                                //NameLength is int?
                            let tmp = prod.NameLength = prod.Name.Length
                                select prod).ToList();
                }
          else {
                Products.ForEach(prod => prod.NameLength = prod.Name.Length);
           }

          ResultText = $"Total Products: {Products.Count}";
        }
        #endregion

    #region ForEachCallingMethod Method
    /// <summary>
    /// Iterate over each object in the collection and call a method to set a property
    /// This method passes in each Product object into the SalesForProduct() method
    /// In the SalesForProduct() method, the total sales for each Product is calculated
    /// The total is placed into each Product objects' ResultText property
    /// </summary>
    public void ForEachCallingMethod()
    {
      if (UseQuerySyntax) {
                Products = (from prod in Products
                            let tmp = prod.TotalSales = SalesForProduct(prod)
                            select prod).ToList();

      }
      else {
                // Method Syntax
                Products.ForEach(prod=>
                            prod.TotalSales = SalesForProduct(prod));
            }

      ResultText = $"Total Products: {Products.Count}";
    }

    /// <summary>
    /// Helper method called by LINQ to sum sales for a product
    /// </summary>
    /// <param name="prod">A product</param>
    /// <returns>Total Sales for Product</returns>
    private decimal SalesForProduct(Product prod)
    { 
        //sales is another list of sales    
        return Sales.Where(sale => sale.ProductID == prod.ProductID)
                  .Sum(sale => sale.LineTotal);
    }
    #endregion

    #region Take Method
    /// <summary>
    /// Use Take() to select a specified number of items from the beginning of a collection
    /// </summary>
    public void Take()
    {
      if (UseQuerySyntax) {
                Products = (from prod in Products
                            let tmp = prod.TotalSales = SalesForProduct(prod)
                            orderby prod.Name
                            select prod).Take(5).ToList();
            }
      else {
        // Method Syntax

      }

      ResultText = $"Total Products: {Products.Count}";
    }
    #endregion

    #region TakeWhile Method
    /// <summary>
    /// Use TakeWhile() to select a specified number of items from the beginning of a collection based on a true condition
    /// </summary>
    public void TakeWhile()
    {
      if (UseQuerySyntax) {
                Products = (from prod in Products
                            let tmp = prod.TotalSales = SalesForProduct(prod)
                            orderby prod.Name
                            select prod).TakeWhile(prod=>prod.Name.Contains("A")).ToList();

            }
      else {
        // Method Syntax

      }

      ResultText = $"Total Products: {Products.Count}";
    }
    #endregion

    #region Skip Method
    /// <summary>
    /// Use Skip() to move past a specified number of items from the beginning of a collection
    /// </summary>
    public void Skip()
    {
      if (UseQuerySyntax) {
                Products = (from prod in Products
                            let tmp = prod.TotalSales = SalesForProduct(prod)
                            orderby prod.Name
                            select prod).Skip(5).ToList();

            }
      else {

            }

      ResultText = $"Total Products: {Products.Count}";
    }
    #endregion

    #region SkipWhile Method
    /// <summary>
    /// Use SkipWhile() to move past a specified number of items from the beginning of a collection based on a true condition
    /// </summary>
    public void SkipWhile()
    {
      if (UseQuerySyntax) {
                Products = (from prod in Products
                            let tmp = prod.TotalSales = SalesForProduct(prod)
                            orderby prod.Name
                            select prod).SkipWhile(prod => prod.Name.Contains("B")).ToList();


            }
            else {
        // Method Syntax

      }

      ResultText = $"Total Products: {Products.Count}";
    }
    #endregion

    #region Distinct
    /// <summary>
    /// The Distinct() operator finds all unique values within a collection
    /// In this sample you put distinct product colors into another collection using LINQ
    /// </summary>
    public void Distinct()
    {
      List<string> colors = new List<string>();

      if (UseQuerySyntax) {
                colors = (from prod in Products
                              select prod.Color).Distinct().ToList();

      }
      else {
                colors = Products.Select(p => p.Color).Distinct().ToList();

      }

      // Build string of Distinct Colors
      foreach (var color in colors) {
        Console.WriteLine($"Color: {color}");
      }
      Console.WriteLine($"Total Colors: {colors.Count}");

      // Clear products
      Products.Clear();
    }
    #endregion

    public void ProductWithALL()
        {
            Products = (from prod in Products
                        where prod.Name.Contains("")
                        select prod).ToList();
            var products = Products.All(prod => prod.Name.Contains(" "));
        }

    public void ProductWithAny(string search)
    {
        //all products have search in name
        bool value = (from prod in Products
                    select prod)
        .All(prod => prod.Name.Contains(search));

        bool find = Products.Any(prod => prod.Name.Contains(search));
    }

        public void ProductWithEqual(string search)
        {
            //all products have search in name
            var value = (from prod in Products
                          where prod.Name == search
                          select prod).ToList();

            var find = Products.Any(prod => prod.Name.Equals(search));
        }
        public void LINQContainsUsingComparer()
        {
            int search = 744;
            bool value, value1;
            ProductIdComparer pc = new ProductIdComparer();
            Product prodToFind = new Product { ProductID = search };
            value = (from prod in Products
                     select prod)
                     .Contains(prodToFind, pc);

            value1 = Products.Contains(prodToFind, pc);
        }
        public void SequenceEqualIntergers()
        {
            bool value;
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5 };
            List<int> list2 = new List<int> { 1, 2, 3, 4, 5 };

            value = (from num in list1
                     select num)
                     .SequenceEqual(list2);
        }

        public void SequenceEqualProdcts()
        {
            bool value;
            List<Product> list1 = new List<Product> { 
                new Product{ProductID = 1, Name="Prod1" },
                new Product{ProductID = 2, Name="Prod2" },
             };
            List<Product> list2 = new List<Product> {
                new Product{ProductID = 1, Name="Prod1" },
                new Product{ProductID = 2, Name="Prod2" },
             };

            value = (from prod in list1
                     select prod)
                     .SequenceEqual(list2);
        }
        public void SequenceEqualProdctsWithCompareClass()
        {
            bool value;
            List<Product> list1 = new List<Product> {
                new Product{ProductID = 1, Name="Prod1" },
                new Product{ProductID = 2, Name="Prod2" },
             };
            List<Product> list2 = new List<Product> {
                new Product{ProductID = 1, Name="Prod1" },
                new Product{ProductID = 2, Name="Prod2" },
                new Product{ProductID = 3, Name="Prod3" }
             };
            ProductIdComparer pc = new ProductIdComparer();
            value = (from prod in list1
                     select prod)
                     .SequenceEqual(list2,pc);
            var except = (from prod in list2
             select prod)
                     .Except(list1, pc);

            var intersect = (from prod in list2
                          select prod)
            .Intersect(list1, pc);

            //not check duplicated
            var concat = (from prod in list2
                             select prod)
                .Concat(list1);

            //check duplicated
            var union = (from prod in list2
                          select prod)
                .Union(list1, pc);
        }
    }

}
