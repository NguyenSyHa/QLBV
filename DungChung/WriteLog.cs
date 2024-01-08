using log4net;
using Newtonsoft.Json;
using System;using QLBV_Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLBV.DungChung
{
    public class WriteLog
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(WriteLog).Name);

        public static bool IsDebugEnabled()
        {
            return _logger.IsDebugEnabled;
        }

        public static bool IsInfoEnabled()
        {
            return _logger.IsInfoEnabled;
        }

        public static void Debug(string message)
        {
            Debug(message, null);
        }

        public static void Debug(Exception ex)
        {
            _logger.Debug(null, ex);
        }

        public static void Debug(string message, Exception ex)
        {
            _logger.Debug(message, ex);
        }

        public static void Info(string message)
        {
            _logger.Info(message);
        }

        public static void Warn(string message)
        {
            Warn(message, null);
        }

        public static void Warn(Exception ex)
        {
            Warn(null, ex);
        }

        public static void Warn(string message, Exception ex)
        {
            _logger.Warn(message, ex);
        }

        public static void Error(string message)
        {
            Error(message, null);
        }

        public static void Error(Exception ex)
        {
            Error(null, ex);
        }

        public static void Error(string message, Exception ex)
        {
            _logger.Error(message, ex);
        }

        public static void Fatal(string message)
        {
            Fatal(message, null);
        }

        public static void Fatal(Exception ex)
        {
            Fatal(null, ex);
        }

        public static void Fatal(string message, Exception ex)
        {
            _logger.Fatal(message, ex);
        }

        public static string TraceData(string name, object data)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("___");
                sb.Append(name + ":");
                sb.Append(JsonConvert.SerializeObject(data));
                sb.Append("___");

                return sb.ToString();
            }
            catch (Exception)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("-----Exception when trace data [" + name + "]-----");
                    return sb.ToString();
                }
                catch (Exception)
                {
                    return "-----Has exception-----";
                }
            }
        }

    }
}
