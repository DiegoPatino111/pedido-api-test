using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace processOrderApi.Interfaces
{
    public interface ILogger
    {
        
        void LogError(string message);
        void LogError(string message, Exception exception);

        
        void LogInfo(string message);
        void LogWarning(string message);
    }
}