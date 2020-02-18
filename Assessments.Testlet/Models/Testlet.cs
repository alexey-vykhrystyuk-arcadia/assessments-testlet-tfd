namespace Assessments.Testlet
{
    using System.Collections.Generic;

    public class Testlet
    {
        public string TestletId { get; }

        public IReadOnlyList<Item> Items { get; }

        internal Testlet(string testletId, IReadOnlyList<Item> items)
        {
            this.TestletId = testletId;
            this.Items = items;
        }
    }
}
