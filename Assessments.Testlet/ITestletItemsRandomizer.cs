using System.Collections.Generic;

namespace Assessments.Testlet
{
    public interface ITestletItemsRandomizer
    {
        IReadOnlyList<Item> Randomize(IReadOnlyList<Item> items);
    }
}
