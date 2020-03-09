using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Helpers.Attributes
{

    public class StringValueAttribute : Attribute
    {

        #region "Properties"

        /// <summary>
        /// Mantiene el un valor string para un valor de un enumerador.
        /// </summary>
        private string _stringValue;
        public string StringValue
        {
            get { return _stringValue; }
            set { _stringValue = value; }
        }

        #endregion

        #region "Constructor"

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Valor inicial del atributo</param>
        /// <remarks></remarks>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }

        #endregion

    }
}
