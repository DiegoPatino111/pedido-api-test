using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace processOrderApi.Logging
{
    public class Log4NetLogger : Interfaces.ILogger
    {
        private readonly ILog _log;

        public Log4NetLogger(ILog log)
        {
            _log = log;
        }

        public void LogError(string message)
        {
            _log.Error(message);
        }

        public void LogError(string message, Exception exception)
        {
            _log.Error(message, exception);
        }

        public void LogInfo(string message)
        {
            _log.Info(message);
        }

        public void LogWarning(string message)
        {
            _log.Warn(message);
        }
    }
}