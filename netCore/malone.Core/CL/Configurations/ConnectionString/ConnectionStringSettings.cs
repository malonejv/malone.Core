using System;
using System.Collections.Generic;
using System.Text;
using malone.Core.CL.Helpers.Extensions;

namespace malone.Core.CL.Configurations.ConnectionString
{
    public class ConnectionStringSettings
    {
        public String Name { get; set; }
        public String ConnectionString { get; set; }
        public String ProviderName { get; set; }

        public ConnectionStringSettings()
        {
        }

        public ConnectionStringSettings(String name, String connectionString)
            : this(name, connectionString, null)
        {
        }

        public ConnectionStringSettings(String name, String connectionString, String providerName)
        {
            this.Name = name;
            this.ConnectionString = connectionString;
            this.ProviderName = providerName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            ConnectionStringSettings otherConnection = (ConnectionStringSettings)obj;

            return string.Equals(Name, otherConnection.Name, StringComparison.InvariantCulture)
                && string.Equals(ConnectionString, otherConnection.ConnectionString, StringComparison.InvariantCulture)
                && string.Equals(ProviderName, otherConnection.ProviderName, StringComparison.InvariantCulture);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Name != null ? Name.GetHashCode(StringComparison.InvariantCulture) : 0);
                hashCode = (hashCode * 397) ^ (ConnectionString != null ? ConnectionString.GetHashCode(StringComparison.InvariantCulture) : 0);
                hashCode = (hashCode * 397) ^ (ProviderName != null ? ProviderName.GetHashCode(StringComparison.InvariantCulture) : 0);
                return hashCode;
            }
        }

        public static bool operator == (ConnectionStringSettings left, ConnectionStringSettings right)
        {
            return Equals(left, right);
        }

        public static bool operator != (ConnectionStringSettings left, ConnectionStringSettings right)
        {
            return !Equals(left, right);
        }
    }
}
