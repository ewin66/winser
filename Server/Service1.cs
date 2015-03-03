using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using Hitearth.Tool;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.IO.Ports;
using System.Threading;

namespace Hitearth
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Ini();
        }
        public void Ini()
        {
            try
            {

                utils.WriteLog("-------------------------------OnStart-------------------------------");

                #region Log
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Log"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Log");
                }
                #endregion

                #region Remoting
                RemotingConfiguration.Configure(AppDomain.CurrentDomain.BaseDirectory + "\\channels.config", true);
                foreach (IChannelReceiver item in ChannelServices.RegisteredChannels)
                {
                    utils.WriteLog(item.GetType().ToString() + "\t" + string.Join("\t", item.GetUrlsForUri("")));
                }
                #endregion

                timer1.Interval = Config.timer1Interval;
                timer1.Enabled = true;

                utils.WriteLog("-------------------------------OnStart-------------------------------");
            }
            catch (Exception ex)
            {
                C.WriteLog(ex.ToString());
            }
        }

        protected override void OnStop()
        {
            try
            {
                foreach (IChannelReceiver item in ChannelServices.RegisteredChannels)
                {
                    ChannelServices.UnregisterChannel(item);
                }
            }
            catch (Exception ex)
            {
                utils.WriteLog(ex.ToString());
            }
        }

        private static object timer1obj = new object();
        public void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer1.Enabled = false;
            lock (timer1obj)
            {
                C.WriteLineLog("-------------开始-------------");
                try
                {
                    // do something
                    C.WriteLineLog("do something");
                }
                catch (Exception ex)
                {
                    C.WriteLineLog(ex.Message);
                }
                C.WriteLineLog("-------------结束-------------");
            }
            timer1.Enabled = true;
        }








    }

}
