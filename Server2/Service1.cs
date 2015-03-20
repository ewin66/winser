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
        private TimerManager<string> timerManager;
        private List<string> users = new List<string>();
        public Service1()
        {
            InitializeComponent();
            timerManager = new TimerManager<string>(timer1, dosomething, users);
            timerManager.Log += new Action<string>(C.WriteLineLog);
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

                users.AddRange(new[] { "张一", "张二", "张三", "张四" });

                timerManager.IniTimer(Config.timer1Interval, true);

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

        public void dosomething(string obj)
        {
            // do something
            C.WriteLineLog(obj + " do something");
        }










    }

}
