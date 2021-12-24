using Framework.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Framework.Services.Logging
{
    public class LogWriter : ILogWriter
    {
        private string m_exePath = string.Empty;
        private readonly AppConfiguration _appConfiguration;
        public LogWriter(IOptionsMonitor<AppConfiguration> appConfiguration)
        {
            _appConfiguration = appConfiguration.CurrentValue;
        }

        private void LogWrite(string logMessage, string type)
        {
            string logFile = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            m_exePath = Path.GetDirectoryName(_appConfiguration.LogfolderURL) + "\\" + logFile;

            if (!System.IO.File.Exists(m_exePath))
            {
                System.IO.File.Create(m_exePath);
            }
            try
            {
                using (StreamWriter w = System.IO.File.AppendText(m_exePath))
                {
                    Log(logMessage, w, type);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Log(string logMessage, TextWriter txtWriter, string type)
        {
            try
            {
                txtWriter.Write("\r\n" + type + " : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Info(string logMessage)
        {
            LogWrite(logMessage, "Info");
        }

        public void Warning(string logMessage)
        {
            LogWrite(logMessage, "Warning");
        }

        public void Error(string logMessage)
        {
            LogWrite(logMessage, "Error");
        }

        public void Message(string logMessage)
        {
            LogWrite(logMessage, "Message");
        }
    }
}
