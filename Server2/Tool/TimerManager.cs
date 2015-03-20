using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Timer = System.Timers.Timer;


namespace Hitearth.Tool
{
    public class TimerManager<T>
    {
        private Timer timer1; private DealPost dealPost; private IEnumerable<T> Ports;
        private Action<string> onLog;
        private void myLog(string content)
        {
            if (onLog != null) onLog(content);
        }
        public event Action<string> Log
        {
            add
            {
                onLog += value;
            }
            remove
            {
                onLog -= value;
            }

        }
        public TimerManager(Timer timer1, DealPost dealPost, IEnumerable<T> Ports)
        {
            if (timer1 == null) throw new ArgumentNullException("timer1");
            if (dealPost == null) throw new ArgumentNullException("dealPost");
            if (Ports == null) throw new ArgumentNullException("Ports");

            this.timer1 = timer1;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.Elapsed);
            this.dealPost = dealPost; this.Ports = Ports;
        }
        public void IniTimer(double timerInterval, bool enabled)
        {
            timer1.Interval = timerInterval;
            timer1.Enabled = enabled;
        }
        private static object timer1obj = new object();
        public void Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer1.Enabled = false;
            lock (timer1obj)
            {
                var beginTime = DateTime.Now;
                myLog(Thread.CurrentThread.ManagedThreadId.ToString() + "定时器开始执行");
                ManualResetEvent[] events = new ManualResetEvent[Ports.Count()]; int i = 0;
                foreach (var port in Ports)
                {
                    events[i] = new ManualResetEvent(false);
                    WaitCallback callback = new WaitCallback(dealWork);
                    ThreadPool.QueueUserWorkItem(callback, new object[] { events[i], port });
                    i++;
                }
                ManualResetEvent.WaitAll(events);
                var dd = DateTime.Now - beginTime;
                myLog(Thread.CurrentThread.ManagedThreadId.ToString() + "定时器结束执行\t历时" + dd.TotalMilliseconds + "毫秒");
            }
            timer1.Enabled = true;
        }
        public delegate void DealPost(T port);
        public void dealWork(object state)
        {
            object[] obj = state as object[];
            ManualResetEvent resetEvent = ((ManualResetEvent)obj[0]);
            T port = (T)(obj[1]);
            try
            {
                dealPost(port);
            }
            finally
            {
                resetEvent.Set();
            }
        }
    }
}
