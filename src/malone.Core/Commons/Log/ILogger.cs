using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Commons.Log
{
    public interface ILogger
    {
        string Name { get; }


        #region Debug

        void Debug(object obj);


        void Debug(string message);

        void Debug(string format, params object[] args);

        void Debug(IFormatProvider provider, string format, params object[] args);

        void Debug(Exception exception);

        void Debug(Exception exception, string message);

        void Debug(Exception exception, string format, params object[] args);

        void Debug(Exception exception, string format, IFormatProvider provider, params object[] args);

        #endregion

        #region Info

        void Info(object obj);

        void Info(string message);

        void Info(string format, params object[] args);

        void Info(IFormatProvider provider, string format, params object[] args);

        void Info(Exception exception);

        void Info(Exception exception, string message);

        void Info(Exception exception, string format, params object[] args);

        void Info(Exception exception, string format, IFormatProvider provider, params object[] args);

        #endregion

        #region Warn

        void Warn(object obj);

        void Warn(string message);

        void Warn(string format, params object[] args);

        void Warn(IFormatProvider provider, string format, params object[] args);

        void Warn(Exception exception);

        void Warn(Exception exception, string message);

        void Warn(Exception exception, string format, params object[] args);

        void Warn(Exception exception, string format, IFormatProvider provider, params object[] args);

        #endregion

        #region Error

        void Error(object obj);

        void Error(string message);

        void Error(string format, params object[] args);

        void Error(IFormatProvider provider, string format, params object[] args);

        void Error(Exception exception);

        void Error(Exception exception, string message);

        void Error(Exception exception, string format, params object[] args);

        void Error(Exception exception, string format, IFormatProvider provider, params object[] args);

        #endregion

        #region Fatal

        void Fatal(object obj);

        void Fatal(string message);

        void Fatal(string format, params object[] args);

        void Fatal(IFormatProvider provider, string format, params object[] args);

        void Fatal(Exception exception);

        void Fatal(Exception exception, string message);

        void Fatal(Exception exception, string format, params object[] args);

        void Fatal(Exception exception, string format, IFormatProvider provider, params object[] args);

        #endregion

        void Log(LogItem item);
    }
}
