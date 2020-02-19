namespace Assessments.Testlet
{
    using System;
    using System.Collections.Generic;

    public class TestletCreator : ITestletCreator
    {
        private readonly ITestletValidator validator;
        private readonly ITestletItemsRandomizer randomizer;

        public TestletCreator(ITestletValidator validator, ITestletItemsRandomizer randomizer)
        {
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
        }

        public Testlet CreateTestlet(string testletId, IReadOnlyList<Item> items)
        {
            _ = testletId ?? throw new ArgumentNullException(nameof(testletId));
            _ = items ?? throw new ArgumentNullException(nameof(items));

            this.validator.ValidateTestletCreationInput(testletId, items);

            var randomItems = this.randomizer.Randomize(items);

            return new Testlet(testletId, randomItems);
        }
    }
}
