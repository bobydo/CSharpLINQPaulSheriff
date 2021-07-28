using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQSamples
{
  public class SamplesViewModel
  {
    #region Constructor
    public SamplesViewModel()
    {
      // Load all Product Data
      Products = ProductRepository.GetAll();
    }
    #endregion

    #region Properties
    public bool UseQuerySyntax { get; set; }
    public List<Product> Products { get; set; }
    public string ResultText { get; set; }
    #endregion

    #region GetAllLooping
    /// <summary>
    /// Put all products into a collection via looping, no LINQ
    /// </summary>
    public void GetAllLooping()
    {
        List<Product> list = new List<Product>();
        foreach (Product item in list)
            Console.WriteLine(item.ToString());
     
        ResultText = $"Total Products: {list.Count}";
    }
    #endregion

    #region GetAll
    /// <summary>
    /// Put all products into a collection using LINQ
    /// </summary>
    public void GetAll()
    {
      List<Product> list = new List<Product>();

      if (UseQuerySyntax) {
        // Query Syntax
        list = (from prod in Products select prod).ToList();
      }
      else {
        // Method Syntax
        list = Products.Select(p => p).ToList();
      }

      ResultText = $"Total Products: {list.Count}";
    }
    #endregion

    #region GetSingleColumn
    /// <summary>
    /// Select a single column
    /// </summary>
    public void GetSingleColumn()
    {
      StringBuilder sb = new StringBuilder(1024);
      List<string> list = new List<string>();

      if (UseQuerySyntax) {
                // Query Syntax
                list.AddRange(from prod in Products select prod.Name);
      }
      else {
                // Method Syntax
                list.AddRange(Products.Select(p => p.Name));
            }

      foreach (string item in list) {
        sb.AppendLine(item);
      }

      ResultText = $"Total Products: {list.Count}" + Environment.NewLine + sb.ToString();
      Products.Clear();
    }
    #endregion

    #region GetSpecificColumns
    /// <summary>
    /// Select a few specific properties from products and create new Product objects
    /// </summary>
    public void GetSpecificColumns()
    {
      if (UseQuerySyntax) {
                Products = (from prod in Products
                            select new Product
                            {
                                ProductID = prod.ProductID,
                                Name = prod.Name,
                                Size = prod.Size
                            }).ToList();
       
      }
      else {
                // Method Syntax
                Products = Products.Select(prod => new Product
                            {
                                ProductID = prod.ProductID,
                                Name = prod.Name,
                                Size = prod.Size
                            }).ToList();
            }

      ResultText = $"Total Products: {Products.Count}";
    }
    #endregion

    #region AnonymousClass
    /// <summary>
    /// Create an anonymous class from selected product properties
    /// </summary>
    public void AnonymousClass()
    {
      StringBuilder sb = new StringBuilder(2048);

      if (UseQuerySyntax) {
                // Query Syntax
                var products = (from prod in Products
                            select new
                            {
                                Identifier = prod.ProductID,
                                ProductName = prod.Name,
                                ProductSize = prod.Size
                            }).ToList();
                // Loop through anonymous class
                foreach (var prod in products)
                {
                    sb.AppendLine($"Product ID: {prod.Identifier}");
                    sb.AppendLine($"   Product Name: {prod.ProductName}");
                    sb.AppendLine($"   Product Size: {prod.ProductSize}");
                }
            }
      else {
                // Method Syntax
                var products = Products.Select(prod =>
                                new
                                {
                                    Identifier = prod.ProductID,
                                    ProductName = prod.Name,
                                    ProductSize = prod.Size
                                }).ToList();
                // Loop through anonymous class
                //foreach (var prod in products) {
                //  sb.AppendLine($"Product ID: {prod.Identifier}");
                //  sb.AppendLine($"   Product Name: {prod.ProductName}");
                //  sb.AppendLine($"   Product Size: {prod.ProductSize}");
                //}
            }

      ResultText = sb.ToString();
      Products.Clear();
    }
    #endregion

    #region OrderBy
    /// <summary>
    /// Order products by Name
    /// </summary>
    public void OrderBy()
    {
      if (UseQuerySyntax) {
                // Query Syntax
                var list = (from prod in Products
                        orderby prod.Name, prod.ListPrice descending
                        select prod ).ToList();
            }
      else {
                // Method Syntax
                var list = Products.OrderBy(p => p.Name)
                    .OrderByDescending(p=>p.ListPrice)
                    .ThenBy(p=>p.Size).ToList();
            }

      ResultText = $"Total Products: {Products.Count}";
    }
    #endregion
  }
}
