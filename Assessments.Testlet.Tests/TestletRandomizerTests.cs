namespace Assessments.Testlet.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class TestletRandomizerTests
    {
        private const int NumberOfFirstPretestItems = 2;

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Randomizer_handles_lack_of_pretest_items_correctly(int numberOfPretestItems)
        {
            var randomizer = this.CreateDefaultTestletItemsRandomizer();

            var items = Enumerable
                .Range(0, numberOfPretestItems)
                .Select(i => new Item { Type = ItemType.Pretest })
                .Union(new [] { new Item { Type = ItemType.Operational } })
                .ToArray();
            var randomizedItems = randomizer.Randomize(items);
            Assert.Equal(numberOfPretestItems, randomizedItems.Where(i => i.Type == ItemType.Pretest).Count());
        }

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

        [Fact]
        public void Randomizer_returns_items_where_remaining_8_out_of_10_items_contains_2_pretest_and_6_operational()
        {
            var randomizer = this.CreateDefaultTestletItemsRandomizer();

            var items = CreateItems(new[]
            {
                ItemType.Operational,
                ItemType.Pretest,
                ItemType.Operational,
                ItemType.Operational,
                ItemType.Pretest,
                ItemType.Operational,
                ItemType.Pretest,
                ItemType.Operational,
                ItemType.Operational,
                ItemType.Pretest,
            });
            var randomizedItems = randomizer.Randomize(items);

            var remainingRandomizedItemsByType = randomizedItems.Skip(NumberOfFirstPretestItems).ToLookup(i => i.Type);
            Assert.Equal(2, remainingRandomizedItemsByType[ItemType.Pretest].Count());
            Assert.Equal(6, remainingRandomizedItemsByType[ItemType.Operational].Count());
        }

        public static IEnumerable<object[]> GetAllButFirstTwoItemsAreRandomlyOrderedTestData => new List<object[]>
        {
            new object[]
            {
                new AllButFirstTwoItemsAreRandomlyOrderedTestData
                {
                    Items = CreateItems(new []
                    {
                        ItemType.Pretest, // 0
                        ItemType.Operational, // 1
                        ItemType.Pretest, // 2
                        ItemType.Pretest, // 3
                        ItemType.Operational, // 4
                        ItemType.Operational, // 5
                    }),
                    PseudoRandomIntegers = new [] { 1, 0, 3, 2, 1, 0 },
                    IndicesWithExpectedOrderOfItems = new [] { 2, 3, 4, 5, 0, 1 },
                }
            },
            new object[]
            {
                new AllButFirstTwoItemsAreRandomlyOrderedTestData
                {
                    Items = CreateItems(new []
                    {
                        ItemType.Operational, // 0
                        ItemType.Pretest, // 1
                        ItemType.Operational, // 2
                        ItemType.Operational, // 3
                        ItemType.Pretest, // 4
                        ItemType.Pretest, // 5
                    }),
                    PseudoRandomIntegers = new [] { 1, 1, 2, 2, 0, 0 },
                    IndicesWithExpectedOrderOfItems = new [] { 4, 5, 3, 2, 0, 1 },
                }
            },
        };

        [Theory]
        [MemberData(nameof(GetAllButFirstTwoItemsAreRandomlyOrderedTestData))]
        public void Randomizer_returns_items_where_all_but_first_2_are_randomly_ordered(AllButFirstTwoItemsAreRandomlyOrderedTestData testData)
        {
            var randomizer = this.CreatePseudoRandomTestletItemsRandomizer(testData.PseudoRandomIntegers);

            var randomizedItems = randomizer.Randomize(testData.Items);

            var allButFirstTwoRandomizedItems = randomizedItems.Skip(NumberOfFirstPretestItems).ToArray();

            var itemsWithExpectedOrder = testData.IndicesWithExpectedOrderOfItems
                .Skip(NumberOfFirstPretestItems)
                .Select(index => testData.Items[index])
                .ToArray();

            Assert.Equal(allButFirstTwoRandomizedItems, itemsWithExpectedOrder);
        }

        private ITestletItemsRandomizer CreateDefaultTestletItemsRandomizer() => new PretestsFirstTestletItemsRandomizer(NumberOfFirstPretestItems, new Random(100500));

        private PretestsFirstTestletItemsRandomizer CreatePseudoRandomTestletItemsRandomizer(IReadOnlyList<int> pseudoRandomIntegers)
            => new PretestsFirstTestletItemsRandomizer(NumberOfFirstPretestItems, new FakeRandom(pseudoRandomIntegers));

        private static Item[] CreateItems(IEnumerable<ItemType> types) => types
            .Select((t, i) => new Item
            {
                ItemId = i.ToString(),
                Type = t,
            })
            .ToArray();

        public class AllButFirstTwoItemsAreRandomlyOrderedTestData
        {
            public IReadOnlyList<Item> Items { get; set; } = Array.Empty<Item>();
            public IReadOnlyList<int> PseudoRandomIntegers { get; set; } = Array.Empty<int>();
            public IReadOnlyList<int> IndicesWithExpectedOrderOfItems { get; set; } = Array.Empty<int>();
        }
    }
}
