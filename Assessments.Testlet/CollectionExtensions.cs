namespace Assessments.Testlet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CollectionExtensions
    {
        public static IEnumerable<int> SelectIndicesWhere<T>(this IEnumerable<T> items, Func<T, int, bool> predicate)
        {
            return items
                .Select((item, index) => (item, index))
                .Where(pair => predicate(pair.item, pair.index))
                .Select(pair => pair.index);
        }
    }
    
}
