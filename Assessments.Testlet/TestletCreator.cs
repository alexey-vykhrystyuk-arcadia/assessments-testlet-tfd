namespace Assessments.Testlet
{
    using System.Collections.Generic;

    public class TestletCreator : ITestletCreator
    {
        private readonly ITestletValidator validator;
        private readonly ITestletItemsRandomizer randomizer;

        public TestletCreator(ITestletValidator validator, ITestletItemsRandomizer randomizer)
        {
            this.validator = validator;
            this.randomizer = randomizer;
        }

        public Testlet CreateTestlet(string testletId, IReadOnlyList<Item> items)
        {
            this.validator.ValidateTestletCreationInput(testletId, items);

            var randomItems = this.randomizer.Randomize(items);

            return new Testlet(testletId, randomItems);
        }
    }
}
