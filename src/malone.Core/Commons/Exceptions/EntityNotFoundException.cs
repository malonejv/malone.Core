using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Exceptions
{
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException()
            : base() { }

        public EntityNotFoundException(string message)
            : base(message) { }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
