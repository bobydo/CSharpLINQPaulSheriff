using System.Collections.Generic;
using System.Linq;

namespace LINQSamples
{
    /// <summary>
    /// extension method for customized filter
    /// </summary>
  public static class ProductHelper
  {
    #region ByColor
    public static IEnumerable<Product> ByColor(this IEnumerable<Product> query, string color)
    {
      return query.Where(prod => prod.Color == color);
    }
    #endregion
  }
}
