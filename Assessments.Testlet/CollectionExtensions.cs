namespace Assessments.Testlet
{
    using System.Collections.Generic;

    public static class CollectionExtensions
    {
        public static void Swap<T>(this IList<T> items, int i, int j)
        {
            var tmp = items[i];
            items[i] = items[j];
            items[j] = tmp;
        }
    }
    
}
