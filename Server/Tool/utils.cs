using System;
using System.IO;
using System.Text;

namespace Hitearth.Tool
{
    public partial class utils
    {

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool WriteLog(string content)
        {
            try
            {
                string filename = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\appLog_" +
                                  DateTime.Now.ToString("yyyy-MM", System.Globalization.DateTimeFormatInfo.InvariantInfo) + ".txt";
                bool append = true;
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff  ", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                StreamWriter sw = new StreamWriter(filename, append, Encoding.UTF8);
                sw.WriteLine(time + content);
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }




    }
}
