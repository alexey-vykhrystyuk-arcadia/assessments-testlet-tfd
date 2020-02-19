namespace Assessments.Testlet.Tests
{
    using System.Linq;
    using Xunit;

    public class TestletValidatorTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(9)]
        public void Validator_throws_exception_when_passed_more_or_less_than_10_items(int numberOfItems)
        {
            var validator = this.CreateDefaultTestletValidator();

            var items = Enumerable
                .Range(0, numberOfItems)
                .Select(i => new Item())
                .ToList();

            void createTestlet() => validator.ValidateTestletCreationInput("TestletId", items);
            var exception = Assert.Throws<TestletMustHaveFixedNumberOfItemsException>(createTestlet);

            Assert.Equal(10, exception.NumberOfItems);
            Assert.NotEmpty(exception.Message);
        }

        [Theory]
        [InlineData(4, ItemType.Pretest, 6, ItemType.Pretest)]
        [InlineData(6, ItemType.Operational, 4, ItemType.Operational)]
        [InlineData(5, ItemType.Operational, 5, ItemType.Pretest)]
        [InlineData(7, ItemType.Operational, 3, ItemType.Pretest)]
        [InlineData(9, ItemType.Operational, 1, ItemType.Pretest)]
        public void Validator_throws_exception_when_passed_items_do_not_contain_6_operational_and_4_pretest(
            int firstItemsCount, 
            ItemType firstItemsType,
            int secondItemsCount, 
            ItemType secondItemsType)
        {
            var validator = this.CreateDefaultTestletValidator();

            var firstItems = Enumerable
                .Range(0, firstItemsCount)
                .Select(i => new Item { Type = firstItemsType });

            var secondItems = Enumerable
                .Range(0, secondItemsCount)
                .Select(i => new Item { Type = secondItemsType });

            var allItems = firstItems.Union(secondItems).ToList();

            void createTestlet() => validator.ValidateTestletCreationInput("TestletId", allItems);
            var exception = Assert.Throws<TestletCreationValidationAggregateException>(createTestlet);

            Assert.NotEmpty(exception.Exceptions);
            Assert.Equal(2, exception.Exceptions.Count);
            Assert.Contains(exception.Exceptions, e => e.ItemsOfType == ItemType.Pretest);
            Assert.Contains(exception.Exceptions, e => e.ItemsOfType == ItemType.Operational);
        }

        private ITestletValidator CreateDefaultTestletValidator() => new TestletValidator();
    }
}
