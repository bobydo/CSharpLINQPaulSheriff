using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples.RepositoryClasses
{
    public static class LinqHelper
    {
        public static IEnumerable<T> Filter<T>(
            this IEnumerable<T> source, Func<T,bool> predicte)
        {
            foreach(var item in source)
            {
                if (predicte(item))
                {
                    yield return item;
                }
            }
        }
    }
}
