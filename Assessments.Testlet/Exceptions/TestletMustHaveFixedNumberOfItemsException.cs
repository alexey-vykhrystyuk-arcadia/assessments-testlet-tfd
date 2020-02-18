namespace Assessments.Testlet
{
    public class TestletMustHaveFixedNumberOfItemsException : TestletCreationValidationException
    {
        public TestletMustHaveFixedNumberOfItemsException(int numberOfItems, ItemType? itemsOfType = default)
            : base(BuildMessage(numberOfItems, itemsOfType))
        {
            this.NumberOfItems = numberOfItems;
            this.ItemsOfType = itemsOfType;
        }

        public int NumberOfItems { get; }

        public ItemType? ItemsOfType { get; }

        private static string BuildMessage(int numberOfItems, ItemType? itemsOfType)
        {
            var typePostFix = itemsOfType != null ? $" of {itemsOfType} type" : string.Empty;
            return $"Testlet must have exactly {numberOfItems} items{typePostFix}";
        }
    }
}
