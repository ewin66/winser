using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hitearth.Tool
{
    public class Config
    {
        public static double timer1Interval = double.Parse(System.Configuration.ConfigurationManager.AppSettings["timer1Interval"]) * 1000;
    }
}
