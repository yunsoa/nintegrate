﻿using System;
using System.IO;
using System.ServiceModel;
using EnterpriseSharedServiceContracts;

namespace EnterpriseSharedServiceImpls
{
    public sealed class LoggingServiceImpl : ILoggingService
    {
        #region ILoggingService Members

        [OperationBehavior]
        public void WriteLog(string message)
        {
            if (string.IsNullOrEmpty(message))
                message = "<Empty>";

            lock (typeof(LoggingServiceImpl))
            {
                File.AppendAllText(GetLogFilePath(), DateTime.Now + "\t" + message + "\r\n");
            }
        }

        private static string GetLogFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logfile.log");
        }

        #endregion
    }
}
