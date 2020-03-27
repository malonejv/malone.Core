using System;
using System.Collections.Generic;
using System.Text;

namespace malone.Core.EL.Identity
{
    public class IdentityMessage
    {
        public string Destination { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
