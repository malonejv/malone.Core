//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:05</date>

using System;
using System.Resources;

namespace malone.Core.Localization
{
	public abstract class LocalizationHandler<TCode> : ILocalizationHandler<TCode>
		where TCode : Enum
	{
		public abstract ResourceManager ResourceManager { get; }

		public LocalizationHandler()
		{
		}

		public string GetString(TCode code, params object[] args)
		{
			string message = "";

			string errorKey = code.ToString();
			message = ResourceManager?.GetString(errorKey);

			if (message != null && args != null && args.Length > 0)
			{
				message = string.Format(message, args);
			}

			return message;
		}
	}
}
