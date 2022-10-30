//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:52</date>

namespace malone.Core.Commons.Exceptions
{
	/// <summary>
	/// Defines the CoreErrors.
	/// </summary>
	internal enum CoreErrors
	{
		/// <summary>
		/// An error occurred while trying to get the section name of type {0}
		/// </summary>
		CONF1 = 1,

		/// <summary>
		/// The section for type {0} is not configured.
		/// </summary>
		CONF2 = 2,

		/// <summary>
		/// Sorry, something went wrong.
		/// {0}: {1}
		/// </summary>
		TECH200 = 200,

		/// <summary>
		/// An unexpected error has ocured.
		/// </summary>
		TECH201 = 201,

		/// <summary>
		/// Method {0} of class {1} is not implemented.
		/// </summary>
		TECH202 = 202,
		//TECH3 = 3,

		/// <summary>
		/// Unauthorized user.
		/// </summary>
		SERVICE300 = 300,

		/// <summary>
		/// An error occurred while trying to get the requested data.
		/// </summary>
		BUSINESS400 = 400,

		/// <summary>
		/// An error occurred while trying to save the requested data.
		/// </summary>
		BUSINESS401 = 401,

		/// <summary>
		/// An error occurred while trying to delete the requested data.
		/// </summary>
		BUSINESS402 = 402,

		/// <summary>
		/// An error occurred while trying to update the requested data.
		/// </summary>
		BUSINESS403 = 403,

		/// <summary>
		/// No results were found for the query made.
		/// </summary>
		BUSINESS404 = 404,

		/// <summary>
		/// Invalid username or password.
		/// </summary>
		BUSINESS405 = 405,

		/// <summary>
		/// You must confirm the email first.
		/// </summary>
		BUSINESS406 = 406,

		/// <summary>
		/// The query to get an entity of type {0}, for id {1} returned no results.
		/// </summary>
		BUSVAL500 = 500,

		/// <summary>
		/// Failed to get an ordered list of type {0}.
		/// </summary>
		DATAACCESS600 = 600,

		/// <summary>
		/// Failed to get an entity of type {0}.
		/// </summary>
		DATAACCESS601 = 601,

		/// <summary>
		/// Error inserting an entity of type {0}.
		/// </summary>
		DATAACCESS602 = 602,

		/// <summary>
		/// Failed to delete an entity of type {0}.
		/// </summary>
		DATAACCESS603 = 603,

		/// <summary>
		/// Failed to update an entity of type {0}.
		/// </summary>
		DATAACCESS604 = 604,

		/// <summary>
		/// Error en la validación de CommandText {0}.
		/// </summary>
		DATAACCESS605 = 605,

		/// <summary>
		/// An error occurred in the service call {0}.
		/// </summary>
		SERVAG700 = 700
	}
}
