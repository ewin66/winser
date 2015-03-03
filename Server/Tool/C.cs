using System;
using System.IO;
using System.Text;

namespace Hitearth.Tool
{
    public class C
    {
        private const int maxnum = 20000;
        private static StringBuilder buff = new StringBuilder(maxnum);
        private static object Cobj = new object();

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool WriteLineLog(string content)
        {
            try
            {
                string time = DateTime.Now.ToString("HH:mm:ss fff  ", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                var s = time + content + "\r\n";
                lock (Cobj)
                {
                    if (buff.Length + s.Length > maxnum) buff.Remove(0, buff.Length);
                    buff.Append(s);
                    Console.Write(s);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool WriteLog(string content)
        {
            try
            {
                string time = DateTime.Now.ToString("HH:mm:ss fff  ", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                var s = time + content;
                lock (Cobj)
                {
                    if (buff.Length + s.Length > maxnum) buff.Remove(0, buff.Length);
                    buff.Append(s);
                    Console.Write(s);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static string GetLog()
        {
            return buff.ToString();
        }

    }
}
