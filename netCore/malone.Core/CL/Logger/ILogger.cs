using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.CL.Logger
{
    public interface ILogger
    {
        string Name { get; }


        #region Debug

        void Debug(object obj);


        void Debug(string message);

        void Debug(string format, params object[] args);

        void Debug(IFormatProvider provider, string format, params object[] args);

        void Debug<TException>(TException exception) where TException : Exception;

        void Debug<TException>(TException exception, string message) where TException : Exception;

        void Debug<TException>(TException exception, string format, params object[] args) where TException : Exception;

        void Debug<TException>(TException exception, string format, IFormatProvider provider, params object[] args) where TException : Exception;

        #endregion

        #region Info

        void Info(object obj);

        void Info(string message);

        void Info(string format, params object[] args);

        void Info(IFormatProvider provider, string format, params object[] args);

        void Info<TException>(TException exception) where TException : Exception;

        void Info<TException>(TException exception, string message) where TException : Exception;

        void Info<TException>(TException exception, string format, params object[] args) where TException : Exception;

        void Info<TException>(TException exception, string format, IFormatProvider provider, params object[] args) where TException : Exception;

        #endregion

        #region Warn

        void Warn(object obj);

        void Warn(string message);

        void Warn(string format, params object[] args);

        void Warn(IFormatProvider provider, string format, params object[] args);

        void Warn<TException>(TException exception) where TException : Exception;

        void Warn<TException>(TException exception, string message) where TException : Exception;

        void Warn<TException>(TException exception, string format, params object[] args) where TException : Exception;

        void Warn<TException>(TException exception, string format, IFormatProvider provider, params object[] args) where TException : Exception;

        #endregion

        #region Error

        void Error(object obj);

        void Error(string message);

        void Error(string format, params object[] args);

        void Error(IFormatProvider provider, string format, params object[] args);

        void Error<TException>(TException exception) where TException : Exception;

        void Error<TException>(TException exception, string message) where TException : Exception;

        void Error<TException>(TException exception, string format, params object[] args) where TException : Exception;

        void Error<TException>(TException exception, string format, IFormatProvider provider, params object[] args) where TException : Exception;

        #endregion

        #region Fatal

        void Fatal(object obj);

        void Fatal(string message);

        void Fatal(string format, params object[] args);

        void Fatal(IFormatProvider provider, string format, params object[] args);

        void Fatal<TException>(TException exception) where TException : Exception;

        void Fatal<TException>(TException exception, string message) where TException : Exception;

        void Fatal<TException>(TException exception, string format, params object[] args) where TException : Exception;

        void Fatal<TException>(TException exception, string format, IFormatProvider provider, params object[] args) where TException : Exception;

        #endregion

        void Log<TException>(LogItem<TException> item) where TException : Exception;
    }
}
