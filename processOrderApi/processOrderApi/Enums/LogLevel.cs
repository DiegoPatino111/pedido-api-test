using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace processOrderApi.Enums
{
    public enum LogLevel
    {
        TRACE = 0,
        DEBUG = 10,
        INFO = 20,
        WARNING = 30,
        ERROR = 40,
        FATAL = 50
    }

    public static class LogLevelExtensions
    {
        public static bool EsError(this LogLevel level) => level >= LogLevel.ERROR;

        public static string ToLog4NetLevel(this LogLevel level)
        {
            return level switch
            {
                LogLevel.TRACE => "TRACE",
                LogLevel.DEBUG => "DEBUG",
                LogLevel.INFO => "INFO",
                LogLevel.WARNING => "WARN",
                LogLevel.ERROR => "ERROR",
                LogLevel.FATAL => "FATAL",
                _ => "INFO"
            };
        }
    }
}