//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:57</date>

namespace malone.Core.Commons.Helpers.Extensions
{
                public static class CharExtensions
    {
                                public const char DefaultSeparator = ';';

                                                        public static string Concat(this char separator, params string[] parameters)
        {
            var result = "";

            foreach (var prm in parameters)
            {
                result += prm + separator;
            }

            result = result.Substring(0, result.Length - 1);

            return result;
        }

                                                        public static string[] Split(this char separator, string value)
        {
            string[] result = value.Split(separator);

            return result;
        }
    }
}
