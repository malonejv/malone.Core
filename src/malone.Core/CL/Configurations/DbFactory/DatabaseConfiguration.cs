﻿using System;

using System.Configuration;

namespace malone.Core.CL.Configurations.DbFactory
{
    //http://www.primaryobjects.com/2007/11/16/implementing-a-database-factory-pattern-in-c-asp-net/
    public sealed class DatabaseConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("Provider")]
        public string Provider
        {
            get { return (string)base["Provider"]; }
        }

        [ConfigurationProperty("ConnectionStringName")]
        public string ConnectionStringName
        {
            get { return (string)base["ConnectionStringName"]; }
        }
        //public string ConnectionString
        //{
        //    get
        //    {
        //        try
        //        {
        //            return ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        //        }
        //        catch (Exception excep)
        //        {
        //            throw new Exception("Connection string " + ConnectionStringName + " was not found in web.config. " + excep.Message);
        //        }
        //    }
        //}
    }
}