using System.Collections.Generic;

namespace Assessments.Testlet
{
    public interface ITestletCreator
    {
        /// <exception cref="Assessments.Testlet.TestletMustHaveFixedNumberOfItemsException"></exception>
        /// <exception cref="Assessments.Testlet.TestletCreationValidationAggregateException"></exception>
        Testlet CreateTestlet(string testletId, IReadOnlyList<Item> items);
    }
}
