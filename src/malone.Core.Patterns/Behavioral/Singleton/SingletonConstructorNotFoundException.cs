using System;

namespace malone.Core.Patterns.Behavioral.Singleton
{
    public class SingletonConstructorNotFoundException : Exception
    {
        private const string ConstructorNotFoundMessage =
            "Singleton<T> derived types require a non-public default constructor.";

        public SingletonConstructorNotFoundException() : base(ConstructorNotFoundMessage)
        {
        }

        public SingletonConstructorNotFoundException(string auxMessage)
: base(string.Format("{0} - {1}", ConstructorNotFoundMessage, auxMessage))
        {
        }

        public SingletonConstructorNotFoundException(string auxMessage, Exception inner)
: base(string.Format("{0} - {1}", ConstructorNotFoundMessage, auxMessage), inner)
        {
        }
    }
}
