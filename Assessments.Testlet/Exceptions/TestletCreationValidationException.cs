namespace Assessments.Testlet
{
    using System;

    public class TestletCreationValidationException : Exception
    {
        public TestletCreationValidationException()
        {

        }

        public TestletCreationValidationException(string message)
            : base(message)
        {
        }
    }
}
