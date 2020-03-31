using System;
using System.Collections.Generic;
using System.Text;

namespace malone.Core.CL.Configurations.Sections
{

    public class FeatureElement 
    {
        public string Name { get; set; }

        public bool AllEnabled { get; set; }

        public BehaviorElementCollection Behaviors { get; private set; }

    }
}
