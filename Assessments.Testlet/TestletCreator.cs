namespace Assessments.Testlet
{
    using System.Collections.Generic;
    using System.Linq;

    public class TestletCreator : ITestletCreator
    {
        private const int FixedNumberOfItemsAllowed = 10;
        private const int NumberOfOperationalItemsAllowed = 6;
        private const int NumberOfPretestItemsAllowed = 4;

        /// <exception cref="Assessments.Testlet.TestletMustHaveFixedNumberOfItemsException"></exception>
        /// <exception cref="Assessments.Testlet.TestletCreationValidationAggregateException"></exception>
        public Testlet CreateTestlet(string testletId, IReadOnlyList<Item> items)
        {
            this.ValidateTestletCreationInput(testletId, items);

            return new Testlet(testletId, items);
        }

        private void ValidateTestletCreationInput(string testletId, IReadOnlyList<Item> items)
        {
            if (items.Count != FixedNumberOfItemsAllowed)
            {
                throw new TestletMustHaveFixedNumberOfItemsException(FixedNumberOfItemsAllowed);
            }

            var exceptions = new List<TestletMustHaveFixedNumberOfItemsException>();
            var itemsByType = items.ToLookup(i => i.Type);

            this.TryAddExceptionIfItemsHaveDifferentCount(
                exceptions,
                expectedCount: NumberOfOperationalItemsAllowed,
                items: itemsByType[ItemType.Operational].ToList(),
                ofType: ItemType.Operational);

            this.TryAddExceptionIfItemsHaveDifferentCount(
                exceptions,
                expectedCount: NumberOfPretestItemsAllowed,
                items: itemsByType[ItemType.Pretest].ToList(),
                ofType: ItemType.Pretest);

            if (exceptions.Count > 0)
            {
                throw new TestletCreationValidationAggregateException(exceptions);
            }
        }

        private bool TryAddExceptionIfItemsHaveDifferentCount(
            ICollection<TestletMustHaveFixedNumberOfItemsException> exceptions,
            int expectedCount,
            IReadOnlyList<Item> items,
            ItemType? ofType = default)
        {
            if (items.Count != expectedCount)
            {
                exceptions.Add(new TestletMustHaveFixedNumberOfItemsException(expectedCount, ofType));
                return true;
            }

            return false;
        }
    }
}
