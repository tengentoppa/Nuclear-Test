using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RobotSettingTest
{
    internal class Logger
    {
        private Action<string> _action;
        private static readonly Action<string> _defaultAction = Console.WriteLine;

        public static Logger Instance { get => new Logger(); }
        public Logger() { _action = _defaultAction; }
        public Logger(Action<string> action) { _action = action; }

        /// <summary>
        /// 設定要寫 log 的方式
        /// 沒設定就預設用 Console.WriteLine
        /// </summary>
        /// <param name="action"></param>
        public void SetWriteLogAction(Action<string> action)
        {
            _action = action;
        }

        public void Log(object obj, Exception exception = null)
        {
            var text = obj is string ? obj as string : JsonConvert.SerializeObject(obj);

            var log = $"{DateTime.Now:HH:mm:ss}\t{text}";

            WriteLog(log);

            if (exception != null)
            {
                WriteLog(new string('-', 20));
                WriteLog(exception.ToString());
                WriteLog(new string('-', 20));
            }
        }

        private void WriteLog(string log)
        {
            (_action ?? _defaultAction)(log);
        }

        public static bool IsDirectory(string path)
        {
            return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
        }
    }
}
