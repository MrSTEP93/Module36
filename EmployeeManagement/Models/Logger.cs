using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EmployeeManagement.Models
{
    internal class Logger : ILogger
    {
        private readonly ReaderWriterLockSlim lock_ = new ReaderWriterLockSlim();

        private string LogDirectory { get; set; }
        private string ErrorFile
        {
            get
            {
                return Path.Combine(LogDirectory, "appLog.txt");
            }
        }

        private string EventFile
        {
            get
            {
                return Path.Combine(LogDirectory, "appLog.txt");
            }
        }
        private string CurDate
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
            }
        }



        public Logger()
        {
            LogDirectory = AppDomain.CurrentDomain.BaseDirectory + @"/_logs/";
            //+ DateTime.Now.ToString("dd-MM-yy HH-mm-ss") + @"/";

            if (!Directory.Exists(LogDirectory))
                Directory.CreateDirectory(LogDirectory);
        }

        public void WriteEvent(string eventMessage)
        {
            lock_.EnterWriteLock();
            try
            {
                using (StreamWriter writer = new StreamWriter(EventFile, append: true))
                {
                    writer.WriteLine(CurDate + ":: INFO: " + eventMessage);
                }
            }
            finally
            {
                lock_.ExitWriteLock();
            }

        }

        public void WriteError(string errorMessage)
        {
            lock_.EnterWriteLock();
            try
            {
                using (StreamWriter writer = new StreamWriter(ErrorFile, append: true))
                {
                    writer.WriteLine(CurDate + ":: ERROR: " + errorMessage);
                }
            }
            finally
            {
                lock_.ExitWriteLock();
            }

        }
    }
}
