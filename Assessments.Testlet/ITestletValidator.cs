using System.Collections.Generic;

namespace Assessments.Testlet
{
    public interface ITestletValidator
    {
        /// <exception cref="Assessments.Testlet.TestletMustHaveFixedNumberOfItemsException"></exception>
        /// <exception cref="Assessments.Testlet.TestletCreationValidationAggregateException"></exception>
        void ValidateTestletCreationInput(string testletId, IReadOnlyList<Item> items);
    }
}
