﻿using malone.Core.CL.Configurations.Attributes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Configurations.DbFactory
{
    [ConfigurationCollection(typeof(DatabaseProviderElement))]
    public class DatabaseProviderElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DatabaseProviderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DatabaseProviderElement)element).Name;
        }
    }
}
