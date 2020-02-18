using System.Collections.Generic;

namespace Assessments.Testlet
{
    public interface ITestletValidator
    {
        void ValidateTestletCreationInput(string testletId, IReadOnlyList<Item> items);
    }
}