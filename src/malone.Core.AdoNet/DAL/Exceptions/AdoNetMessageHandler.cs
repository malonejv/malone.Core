﻿using malone.Core.CL.Exceptions.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.AdoNet.DAL.Exceptions
{
    internal class AdoNetMessageHandler : MessageHandler<AdoNetErrors>, IAdoNetMessageHandler
    {
    }
}
