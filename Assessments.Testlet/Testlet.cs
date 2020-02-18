namespace Assessments.Testlet
{
    using System.Collections.Generic;

    public class Testlet
    {
        public string TestletId { get; }

        public IReadOnlyList<Item> Items { get; }

        internal Testlet(string testletId, IReadOnlyList<Item> items)
        {
            TestletId = testletId;
            Items = items;
        }

        public List<Item> Randomize()
        {
            //Items private collection has 6 Operational and 4 Pretest Items. Randomize the order of these items as per the requirement (with TDD)
            //The assignment will be reviewed on the basis of – Tests written first, Correct logic, Well structured & clean readable code.
            return new List<Item>();
        }
    }
}
