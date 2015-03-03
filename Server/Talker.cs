using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hitearth.Tool;
using System.IO.Ports;

namespace Hitearth
{
    public class Talker : MarshalByRefObject, ILog
    {
        public string Ping(string word)
        {
            return word + "\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff");
        }

        public string GetLog()
        {
            return C.GetLog();
        }
    }
}
