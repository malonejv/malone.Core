using System;

namespace malone.Core.Patterns.Behavioral.Singleton
{
    public class SingletonConstructorNotFoundException : Exception
    {
        private const string ConstructorNotFoundMessage =
            "Singleton<T> derived types require a non-public default constructor.";

        /// <summary>
        ///     Initializes a new instance of the
        ///     BSR.CRMBE.Core.Exceptions.ConstructorNotFoundException class.
        /// </summary>
        public SingletonConstructorNotFoundException() : base(ConstructorNotFoundMessage)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     BSR.CRMBE.Core.Exceptions.ConstructorNotFoundException class.
        /// </summary>
        /// <param name="auxMessage"> Message describing the auxiliary. </param>
        public SingletonConstructorNotFoundException(string auxMessage)
            : base(string.Format("{0} - {1}", ConstructorNotFoundMessage, auxMessage))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     BSR.CRMBE.Core.Exceptions.ConstructorNotFoundException class.
        /// </summary>
        /// <param name="auxMessage"> Message describing the auxiliary. </param>
        /// <param name="inner">      The inner. </param>
        public SingletonConstructorNotFoundException(string auxMessage, Exception inner)
            : base(string.Format("{0} - {1}", ConstructorNotFoundMessage, auxMessage), inner)
        {
        }
    }
}
