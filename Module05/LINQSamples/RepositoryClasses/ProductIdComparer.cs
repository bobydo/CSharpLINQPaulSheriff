using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples.RepositoryClasses
{
    public class ProductIdComparer : EqualityComparer<Product>
    {
        public override bool Equals(Product x, Product y)
        {
            return (x.ProductID == y.ProductID && x.Name == y.Name);
        }

        public override int GetHashCode([DisallowNull] Product obj)
        {
            return obj.ProductID.GetHashCode();
        }
    }
}
