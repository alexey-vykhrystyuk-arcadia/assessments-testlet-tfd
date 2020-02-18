namespace Assessments.Testlet
{
    using System.Collections.Generic;

    public class TestletCreationValidationAggregateException : TestletCreationValidationException
    {
        public TestletCreationValidationAggregateException(IReadOnlyCollection<TestletMustHaveFixedNumberOfItemsException> exceptions)
            : base()
        {
            this.Exceptions = exceptions;
        }

        public IReadOnlyCollection<TestletMustHaveFixedNumberOfItemsException> Exceptions { get; }
    }
}
