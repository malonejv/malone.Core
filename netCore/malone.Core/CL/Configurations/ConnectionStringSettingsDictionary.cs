using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace malone.Core.CL.Configurations
{
    public sealed class ConnectionStringSettingsDictionary : IDictionary<string, ConnectionStringSettings>
    {
        private readonly Dictionary<string, ConnectionStringSettings> _Connectionstrings;

        public ConnectionStringSettingsDictionary()
        {
            _Connectionstrings = new Dictionary<string, ConnectionStringSettings>();
        }

        public ConnectionStringSettingsDictionary(int capacity)
        {
            _Connectionstrings = new Dictionary<string, ConnectionStringSettings>(capacity);
        }

        #region IEnumerable methods

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)_Connectionstrings).GetEnumerator();
        }

        #endregion

        #region IEnumerable<> methods

        IEnumerator<KeyValuePair<string, ConnectionStringSettings>> IEnumerable<KeyValuePair<string, ConnectionStringSettings>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, ConnectionStringSettings>>)_Connectionstrings).GetEnumerator();
        }

        #endregion

        #region ICollection<> methods

        void ICollection<KeyValuePair<string, ConnectionStringSettings>>.Add(KeyValuePair<string, ConnectionStringSettings> item)
        {
            ((ICollection<KeyValuePair<string, ConnectionStringSettings>>)_Connectionstrings).Add(item);
        }

        void ICollection<KeyValuePair<string, ConnectionStringSettings>>.Clear()
        {
            ((ICollection<KeyValuePair<string, ConnectionStringSettings>>)_Connectionstrings).Clear();
        }

        Boolean ICollection<KeyValuePair<string, ConnectionStringSettings>>.Contains(KeyValuePair<string, ConnectionStringSettings> item)
        {
            return ((ICollection<KeyValuePair<string, ConnectionStringSettings>>)_Connectionstrings).Contains(item);
        }

        void ICollection<KeyValuePair<string, ConnectionStringSettings>>.CopyTo(KeyValuePair<string, ConnectionStringSettings>[] array, Int32 arrayIndex)
        {
            ((ICollection<KeyValuePair<string, ConnectionStringSettings>>)_Connectionstrings).CopyTo(array, arrayIndex);
        }

        Boolean ICollection<KeyValuePair<string, ConnectionStringSettings>>.Remove(KeyValuePair<string, ConnectionStringSettings> item)
        {
            return ((ICollection<KeyValuePair<string, ConnectionStringSettings>>)_Connectionstrings).Remove(item);
        }

        public Int32 Count => ((ICollection<KeyValuePair<string, ConnectionStringSettings>>)_Connectionstrings).Count;
        public Boolean IsReadOnly => ((ICollection<KeyValuePair<string, ConnectionStringSettings>>)_Connectionstrings).IsReadOnly;
        #endregion

        #region IDictionary<> methods
        public void Add(string key, ConnectionStringSettings value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            // NOTE only slight modification, we add back in the Name of connectionstring here (since it is the key)
            value.Name = key;
            _Connectionstrings.Add(key, value);
        }

        public Boolean ContainsKey(string key)
        {
            return _Connectionstrings.ContainsKey(key);
        }

        public Boolean Remove(string key)
        {
            return _Connectionstrings.Remove(key);
        }

        public Boolean TryGetValue(string key, out ConnectionStringSettings value)
        {
            return _Connectionstrings.TryGetValue(key, out value);
        }

        public ConnectionStringSettings this[string key]
        {
            get => _Connectionstrings[key];
            set => Add(key, value);
        }

        public ICollection<string> Keys => _Connectionstrings.Keys;
        public ICollection<ConnectionStringSettings> Values => _Connectionstrings.Values;
        #endregion
    }
}
