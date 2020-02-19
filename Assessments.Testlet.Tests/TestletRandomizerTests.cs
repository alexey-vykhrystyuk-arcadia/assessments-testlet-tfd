namespace Assessments.Testlet.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class TestletRandomizerTests
    {
        private const int NumberOfFirstPretestItems = 2;

        public static IEnumerable<object[]> GetFirstTwoItemsAreAlwaysOfPretestTypeTestData => new List<object[]>
        {
            new object[]
            {
                CreateItems(new []
                {
                    ItemType.Pretest,
                    ItemType.Operational,
                    ItemType.Pretest,
                    ItemType.Pretest,
                    ItemType.Operational,
                    ItemType.Operational,
                }),
            },

            new object[]
            {
                CreateItems(new []
                {
                    ItemType.Operational,
                    ItemType.Pretest,
                    ItemType.Operational,
                    ItemType.Operational,
                    ItemType.Pretest,
                    ItemType.Pretest,
                }),
            },
        };

        [Theory]
        [MemberData(nameof(GetFirstTwoItemsAreAlwaysOfPretestTypeTestData))]
        public void Randomizer_returns_items_where_first_2_are_always_of_pretest_type(Item[] items)
        {
            var randomizer = this.CreateDefaultTestletItemsRandomizer();

            var randomizedItems = randomizer.Randomize(items);

            var firstTwoRandomizedItems = randomizedItems.Take(NumberOfFirstPretestItems).ToList();
            Assert.True(firstTwoRandomizedItems.All(i => i.Type == ItemType.Pretest));
            Assert.All(firstTwoRandomizedItems, i => Assert.Contains(i, items));
        }


        public static IEnumerable<object[]> GetAllButFirstTwoItemsAreRandomlyOrderedTestData => new List<object[]>
        {
            new object[]
            {
                // items:
                CreateItems(new []
                {
                    ItemType.Pretest, // 0
                    ItemType.Operational, // 1
                    ItemType.Pretest, // 2
                    ItemType.Pretest, // 3
                    ItemType.Operational, // 4
                    ItemType.Operational, // 5
                }),

                // pseudoRandomIntegers:
                new [] { 1, 0, 3, 2, 1, 0 },

                // indicesWithExpectedOrderOfItems:
                new [] { 2, 0, 5, 4, 3, 1 },
            },

            new object[]
            {
                // items:
                CreateItems(new []
                {
                    ItemType.Operational, // 0
                    ItemType.Pretest, // 1
                    ItemType.Operational, // 2
                    ItemType.Operational, // 3
                    ItemType.Pretest, // 4
                    ItemType.Pretest, // 5
                }),  

                // pseudoRandomIntegers:
                new [] { 1, 1, 2, 2, 0, 0 },

                // indicesWithExpectedOrderOfItems:
                new [] { 4, 5, 2, 3, 0, 1 },
            },
        };

        [Theory]
        [MemberData(nameof(GetAllButFirstTwoItemsAreRandomlyOrderedTestData))]
        public void Randomizer_returns_items_where_all_but_first_2_are_randomly_ordered(Item[] items, int[] pseudoRandomIntegers, int[] indicesWithExpectedOrderOfItems)
        {
            var randomizer = this.CreatePseudoRandomTestletItemsRandomizer(pseudoRandomIntegers);

            var randomizedItems = randomizer.Randomize(items);

            var allButFirstTwoRandomizedItems = randomizedItems.Skip(NumberOfFirstPretestItems).ToArray();

            var itemsWithExpectedOrder = indicesWithExpectedOrderOfItems
                .Skip(NumberOfFirstPretestItems)
                .Select(index => items[index])
                .ToArray();

            Assert.Equal(allButFirstTwoRandomizedItems, itemsWithExpectedOrder);
        }

        private ITestletItemsRandomizer CreateDefaultTestletItemsRandomizer() => new TestletItemsRandomizer();

        private TestletItemsRandomizer CreatePseudoRandomTestletItemsRandomizer(IReadOnlyList<int> pseudoRandomIntegers) => new TestletItemsRandomizer(
            new FakeRandom(pseudoRandomIntegers));

        private static IReadOnlyList<Item> CreateItems(IEnumerable<ItemType> types) => types
            .Select((t, i) => new Item
            {
                ItemId = i.ToString(),
                Type = t,
            })
            .ToArray();
    }
}
