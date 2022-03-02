//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:08</date>

using System;

namespace malone.Core.Logging
{
    public abstract class LoggerBase : ICoreLogger
    {
        private const string NullString = "null";

        public string Name { get; private set; }

        protected LoggerBase(string name)
        {
            Name = name;
        }

        protected LoggerBase()
        {
        }

        public void Debug(object obj)
        {
            if (IsLogLevelEnabled(LogLevel.Debug))
            {
                Log(LogLevel.Debug, obj != null ? obj.ToString() : NullString);
            }
        }

        public void Debug(string message)
        {
            if (IsLogLevelEnabled(LogLevel.Debug))
            {
                Log(LogLevel.Debug, message);
            }
        }

        public void Debug(string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Debug))
            {
                Log(LogLevel.Debug, format, args);
            }
        }

        public void Debug(IFormatProvider provider, string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Debug))
            {
                Log(LogLevel.Debug, provider, format, args);
            }
        }

        public void Debug(Exception exception)
        {
            if (IsLogLevelEnabled(LogLevel.Debug))
            {
                Log(LogLevel.Debug, exception);
            }
        }

        public void Debug(Exception exception, string message)
        {
            if (IsLogLevelEnabled(LogLevel.Debug))
            {
                Log(LogLevel.Debug, exception, message);
            }
        }

        public void Debug(Exception exception, string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Debug))
            {
                Log(LogLevel.Debug, exception, format, args);
            }
        }

        public void Debug(Exception exception, string format, IFormatProvider provider, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Debug))
            {
                Log(LogLevel.Debug, exception, provider, format, args);
            }
        }

        public void Info(object obj)
        {
            if (IsLogLevelEnabled(LogLevel.Info))
            {
                Log(LogLevel.Info, obj != null ? obj.ToString() : NullString);
            }
        }

        public void Info(string message)
        {
            if (IsLogLevelEnabled(LogLevel.Info))
            {
                Log(LogLevel.Info, message);
            }
        }

        public void Info(string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Info))
            {
                Log(LogLevel.Info, format, args);
            }
        }

        public void Info(IFormatProvider provider, string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Info))
            {
                Log(LogLevel.Info, provider, format, args);
            }
        }

        public void Info(Exception exception)
        {
            if (IsLogLevelEnabled(LogLevel.Info))
            {
                Log(LogLevel.Info, exception);
            }
        }

        public void Info(Exception exception, string message)
        {
            if (IsLogLevelEnabled(LogLevel.Info))
            {
                Log(LogLevel.Info, exception, message);
            }
        }

        public void Info(Exception exception, string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Info))
            {
                Log(LogLevel.Info, exception, format, args);
            }
        }

        public void Info(Exception exception, string format, IFormatProvider provider, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Info))
            {
                Log(LogLevel.Info, exception, provider, format, args);
            }
        }

        public void Warn(object obj)
        {
            if (IsLogLevelEnabled(LogLevel.Warn))
            {
                Log(LogLevel.Warn, obj != null ? obj.ToString() : NullString);
            }
        }

        public void Warn(string message)
        {
            if (IsLogLevelEnabled(LogLevel.Warn))
            {
                Log(LogLevel.Warn, message);
            }
        }

        public void Warn(string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Warn))
            {
                Log(LogLevel.Warn, format, args);
            }
        }

        public void Warn(IFormatProvider provider, string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Warn))
            {
                Log(LogLevel.Warn, provider, format, args);
            }
        }

        public void Warn(Exception exception)
        {
            if (IsLogLevelEnabled(LogLevel.Warn))
            {
                Log(LogLevel.Warn, exception);
            }
        }

        public void Warn(Exception exception, string message)
        {
            if (IsLogLevelEnabled(LogLevel.Warn))
            {
                Log(LogLevel.Warn, exception, message);
            }
        }

        public void Warn(Exception exception, string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Warn))
            {
                Log(LogLevel.Warn, exception, format, args);
            }
        }

        public void Warn(Exception exception, string format, IFormatProvider provider, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Warn))
            {
                Log(LogLevel.Warn, exception, provider, format, args);
            }
        }

        public void Error(object obj)
        {
            if (IsLogLevelEnabled(LogLevel.Error))
            {
                Log(LogLevel.Error, obj != null ? obj.ToString() : NullString);
            }
        }

        public void Error(string message)
        {
            if (IsLogLevelEnabled(LogLevel.Error))
            {
                Log(LogLevel.Error, message);
            }
        }

        public void Error(string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Error))
            {
                Log(LogLevel.Error, format, args);
            }
        }

        public void Error(IFormatProvider provider, string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Error))
            {
                Log(LogLevel.Error, provider, format, args);
            }
        }

        public void Error(Exception exception)
        {
            if (IsLogLevelEnabled(LogLevel.Error))
            {
                Log(LogLevel.Error, exception);
            }
        }

        public void Error(Exception exception, string message)
        {
            if (IsLogLevelEnabled(LogLevel.Error))
            {
                Log(LogLevel.Error, exception, message);
            }
        }

        public void Error(Exception exception, string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Error))
            {
                Log(LogLevel.Error, exception, format, args);
            }
        }

        public void Error(Exception exception, string format, IFormatProvider provider, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Error))
            {
                Log(LogLevel.Error, exception, provider, format, args);
            }
        }

        public void Fatal(object obj)
        {
            if (IsLogLevelEnabled(LogLevel.Fatal))
            {
                Log(LogLevel.Fatal, obj != null ? obj.ToString() : NullString);
            }
        }

        public void Fatal(string message)
        {
            if (IsLogLevelEnabled(LogLevel.Fatal))
            {
                Log(LogLevel.Fatal, message);
            }
        }

        public void Fatal(string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Fatal))
            {
                Log(LogLevel.Fatal, format, args);
            }
        }

        public void Fatal(IFormatProvider provider, string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Fatal))
            {
                Log(LogLevel.Fatal, provider, format, args);
            }
        }

        public void Fatal(Exception exception)
        {
            if (IsLogLevelEnabled(LogLevel.Fatal))
            {
                Log(LogLevel.Fatal, exception);
            }
        }

        public void Fatal(Exception exception, string message)
        {
            if (IsLogLevelEnabled(LogLevel.Fatal))
            {
                Log(LogLevel.Fatal, exception, message);
            }
        }

        public void Fatal(Exception exception, string format, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Fatal))
            {
                Log(LogLevel.Fatal, exception, format, args);
            }
        }

        public void Fatal(Exception exception, string format, IFormatProvider provider, params object[] args)
        {
            if (IsLogLevelEnabled(LogLevel.Fatal))
            {
                Log(LogLevel.Fatal, exception, provider, format, args);
            }
        }

        public virtual void Log(LogLevel level, string format, params object[] args)
        {
            Log(level, String.Format(format, args));
        }

        public virtual void Log(LogLevel level, IFormatProvider provider, string format, params object[] args)
        {
            Log(level, String.Format(provider, format, args));
        }

        public virtual void Log(LogLevel level, string message)
        {
            LogItem item = new LogItem { LogLevel = level, Message = message, LoggerName = Name };
            Log(item);
        }

        public virtual void Log(LogLevel level, Exception exception)
        {
            Log(level, exception, String.Empty);
        }

        public virtual void Log(LogLevel level, Exception exception, string format, params object[] args)
        {
            string message = String.Format(format, args);
            Log(level, exception, message);
        }

        public virtual void Log(LogLevel level, Exception exception, IFormatProvider provider, string format,
params object[] args)
        {
            string message = String.Format(provider, format, args);
            Log(level, exception, message);
        }

        public virtual void Log(LogLevel level, Exception exception, string message)
        {
            Type exceptionType = typeof(Exception);
            var errorMessagePropertyInfo = exceptionType.GetProperty("ErrorMessage");
            if (errorMessagePropertyInfo != null)
            {
                message = errorMessagePropertyInfo.GetValue(exception).ToString();
            }


            LogItem item = new LogItem { LogLevel = level, Message = message, LoggerName = this.Name };
            item.Exception = exception;
            Log(item);
        }

        public abstract void Log(LogItem item);

        protected virtual bool IsLogLevelEnabled(LogLevel level)
        {
            return true;
        }
    }
}
