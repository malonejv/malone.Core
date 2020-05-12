﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Identity.Entities
{
    public class AuthenticationProperties<TKey>
        where TKey : IEquatable<TKey>, IConvertible
    {
        internal const string IssuedUtcKey = ".issued";
        internal const string ExpiresUtcKey = ".expires";
        internal const string IsPersistentKey = ".persistent";
        internal const string RedirectUriKey = ".redirect";
        internal const string RefreshKey = ".refresh";
        internal const string UtcDateTimeFormat = "r";

        private readonly IDictionary<string, TKey> _dictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationProperties"/> class
        /// </summary>
        public AuthenticationProperties()
        {
            _dictionary = new Dictionary<string, TKey>(StringComparer.Ordinal);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationProperties"/> class
        /// </summary>
        /// <param name="dictionary"></param>
        public AuthenticationProperties(IDictionary<string, TKey> dictionary)
        {
            _dictionary = dictionary ?? new Dictionary<string, TKey>(StringComparer.Ordinal);
        }

        /// <summary>
        /// State values about the authentication session.
        /// </summary>
        public IDictionary<string, TKey> Dictionary
        {
            get { return _dictionary; }
        }

        /// <summary>
        /// Gets or sets whether the authentication session is persisted across multiple requests.
        /// </summary>
        public bool IsPersistent
        {
            get { return _dictionary.ContainsKey(IsPersistentKey); }
            set
            {
                if (_dictionary.ContainsKey(IsPersistentKey))
                {
                    if (!value)
                    {
                        _dictionary.Remove(IsPersistentKey);
                    }
                }
                else
                {
                    if (value)
                    {
                        _dictionary.Add(IsPersistentKey, default(TKey));
                    }
                }
            }
        }

        public TKey RedirectUri
        {
            get
            {
                TKey value;
                return _dictionary.TryGetValue(RedirectUriKey, out value) ? value : default(TKey);
            }
            set
            {
                if (value != null)
                {
                    _dictionary[RedirectUriKey] = value;
                }
                else
                {
                    if (_dictionary.ContainsKey(RedirectUriKey))
                    {
                        _dictionary.Remove(RedirectUriKey);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the time at which the authentication ticket was issued.
        /// </summary>
        public DateTimeOffset? IssuedUtc
        {
            get
            {
                TKey value;
                if (_dictionary.TryGetValue(IssuedUtcKey, out value))
                {
                    DateTimeOffset dateTimeOffset;
                    if (DateTimeOffset.TryParseExact(value, UtcDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dateTimeOffset))
                    {
                        return dateTimeOffset;
                    }
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    _dictionary[IssuedUtcKey] = value.Value.ToString(UtcDateTimeFormat, CultureInfo.InvariantCulture);
                }
                else
                {
                    if (_dictionary.ContainsKey(IssuedUtcKey))
                    {
                        _dictionary.Remove(IssuedUtcKey);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the time at which the authentication ticket expires.
        /// </summary>
        public DateTimeOffset? ExpiresUtc
        {
            get
            {
                TKey value;
                if (_dictionary.TryGetValue(ExpiresUtcKey, out value))
                {
                    DateTimeOffset dateTimeOffset;
                    if (DateTimeOffset.TryParseExact(value, UtcDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dateTimeOffset))
                    {
                        return dateTimeOffset;
                    }
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    _dictionary[ExpiresUtcKey] = value.Value.ToString(UtcDateTimeFormat, CultureInfo.InvariantCulture);
                }
                else
                {
                    if (_dictionary.ContainsKey(ExpiresUtcKey))
                    {
                        _dictionary.Remove(ExpiresUtcKey);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets if refreshing the authentication session should be allowed.
        /// </summary>
        public bool? AllowRefresh
        {
            get
            {
                TKey value;
                if (_dictionary.TryGetValue(RefreshKey, out value))
                {
                    bool refresh;
                    if (bool.TryParse(value, out refresh))
                    {
                        return refresh;
                    }
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    _dictionary[RefreshKey] = value.Value.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    if (_dictionary.ContainsKey(RefreshKey))
                    {
                        _dictionary.Remove(RefreshKey);
                    }
                }
            }
        }
    }
}
