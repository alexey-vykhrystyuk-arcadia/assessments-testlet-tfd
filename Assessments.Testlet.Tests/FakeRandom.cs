namespace Assessments.Testlet.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FakeRandom : Random
    {
        private readonly List<int> pseudoRandomIntegers;

        public FakeRandom(IReadOnlyList<int> pseudoRandomIntegers)
        {
            this.pseudoRandomIntegers = pseudoRandomIntegers.ToList();
        }

        public override int Next(int maxValue)
        {
            var result = this.pseudoRandomIntegers[0];
            this.pseudoRandomIntegers.RemoveAt(0);
            return result;
        }
    }
}
