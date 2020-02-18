using System.Collections.Generic;

namespace Assessments.Testlet
{
    public interface ITestletCreator
    {
        Testlet CreateTestlet(string testletId, IReadOnlyList<Item> items);
    }
}
