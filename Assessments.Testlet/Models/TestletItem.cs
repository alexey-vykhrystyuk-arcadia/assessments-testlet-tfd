namespace Assessments.Testlet
{
    using System.Diagnostics;

    [DebuggerDisplay("ItemId = {ItemId} | Type = {Type}")]
    public class Item
    {
        public string ItemId { get; set; } = string.Empty;

        public ItemType Type { get; set; }
    }
}
