//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:53</date>

using System;

namespace malone.Core.Commons.Exceptions
{
                public class EntityNotFoundException : BaseException
    {
                                public EntityNotFoundException()
            : base()
        {
        }

                                        public EntityNotFoundException(string message)
            : base(message)
        {
        }

                                                public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
