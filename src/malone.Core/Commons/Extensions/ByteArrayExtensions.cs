//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:56</date>

using System.Text;

namespace malone.Core.Commons.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string ToHexString(this byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }
    }
}
