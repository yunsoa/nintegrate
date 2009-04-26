using System;
using System.IO;
using System.ServiceModel;
using EnterpriseSharedServiceContracts;

namespace EnterpriseSharedServiceImpls
{
    public sealed class LoggingServiceImpl : ILoggingService
    {
        private static readonly object _syncLock = new object();

        #region ILoggingService Members

        [OperationBehavior]
        public void WriteLog(string message)
        {
            if (string.IsNullOrEmpty(message))
                message = "<Empty>";

            lock (_syncLock)
            {
                File.AppendAllText(GetLogFilePath(), DateTime.Now + "\t" + message);
            }
        }

        private string GetLogFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logfile.log");
        }

        #endregion
    }
}
