namespace Assessments.Testlet
{
    using System.Collections.Generic;

    public class TestletCreator : ITestletCreator
    {
        private readonly ITestletValidator validator;

        public TestletCreator(ITestletValidator validator)
        {
            this.validator = validator;
        }

        /// <exception cref="Assessments.Testlet.TestletMustHaveFixedNumberOfItemsException"></exception>
        /// <exception cref="Assessments.Testlet.TestletCreationValidationAggregateException"></exception>
        public Testlet CreateTestlet(string testletId, IReadOnlyList<Item> items)
        {
            this.validator.ValidateTestletCreationInput(testletId, items);

            return new Testlet(testletId, items);
        }
    }
}
