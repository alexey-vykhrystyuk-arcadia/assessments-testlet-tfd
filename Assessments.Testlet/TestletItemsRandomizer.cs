namespace Assessments.Testlet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TestletItemsRandomizer : ITestletItemsRandomizer
    {
        private const int NumberOfFirstPretestItems = 2;
        private readonly Random random;

        public TestletItemsRandomizer(Random? random = default)
        {
            this.random = random ?? new Random();
        }

        public IReadOnlyList<Item> Randomize(IReadOnlyList<Item> items)
        {
            _ = items ?? throw new ArgumentNullException(nameof(items));

            var randomizedItems = new List<Item>();

            var pretestItemIndicesToRandomize = items
                .SelectIndicesWhere((item, index) => item.Type == ItemType.Pretest)
                .ToList();

            var randomizedPretestItemIndices = new int[NumberOfFirstPretestItems];

            for (int i = 0; i < NumberOfFirstPretestItems; i++)
            {
                var pretestItemIndex = this.TakeRandomIndex(pretestItemIndicesToRandomize);
                randomizedItems.Add(items[pretestItemIndex]);
                randomizedPretestItemIndices[i] = pretestItemIndex;
            }

            var otherItemIndicesToRandomize = items
                .SelectIndicesWhere((item, index) => !randomizedPretestItemIndices.Contains(index))
                .ToList();

            for (int i = 0; i < items.Count - NumberOfFirstPretestItems; i++)
            {
                var itemIndex = this.TakeRandomIndex(otherItemIndicesToRandomize);
                randomizedItems.Add(items[itemIndex]);
            }

            return randomizedItems;
        }

        private int TakeRandomIndex(IList<int> indicesToRandomize)
        {
            var randomIndex = this.random.Next(indicesToRandomize.Count);
            var index = indicesToRandomize[randomIndex];
            indicesToRandomize.RemoveAt(randomIndex);
            return index;
        }
    }
    
}
