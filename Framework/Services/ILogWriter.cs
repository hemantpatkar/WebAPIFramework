using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services
{
    public interface ILogWriter
    {
        void Info(string logMessage);
        void Warning(string logMessage);
        void Error(string logMessage);
        void Message(string logMessage);
    }
}
