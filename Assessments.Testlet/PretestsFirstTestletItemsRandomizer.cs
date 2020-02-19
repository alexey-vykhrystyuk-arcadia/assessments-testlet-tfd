namespace Assessments.Testlet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PretestsFirstTestletItemsRandomizer : ITestletItemsRandomizer
    {
        private readonly int numberOfFirstPretestItems;
        private readonly Random random;

        public PretestsFirstTestletItemsRandomizer(int numberOfFirstPretestItems, Random? random = default)
        {
            this.numberOfFirstPretestItems = numberOfFirstPretestItems;
            this.random = random ?? new Random();
        }

        public IReadOnlyList<Item> Randomize(IReadOnlyList<Item> items)
        {
            _ = items ?? throw new ArgumentNullException(nameof(items));

            var shuffledItems = this.ShuffleItems(items);

            var numberOfPretestItemsMovedToStart = 0;
            for (int i = 0; i < shuffledItems.Count; i++)
            {
                if (shuffledItems[i].Type == ItemType.Pretest && numberOfPretestItemsMovedToStart < this.numberOfFirstPretestItems)
                {
                    shuffledItems.Swap(i, numberOfPretestItemsMovedToStart++);
                }
            }

            return shuffledItems;
        }

        private List<Item> ShuffleItems(IEnumerable<Item> items)
        {
            var shuffledItems = items.ToList();

            for (var i = shuffledItems.Count - 1; i >= 0; i--)
            {
                int randomIndex = this.random.Next(i + 1);
                shuffledItems.Swap(randomIndex, i);
            }

            return shuffledItems;
        }
    }
}
