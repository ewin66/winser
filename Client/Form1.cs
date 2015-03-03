using System;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using Hitearth.Tool;

namespace Hitearth
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private ILog talk;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                RemotingConfiguration.Configure("channels.config", true);

                var aaa = ChannelServices.RegisteredChannels[0] as IChannelSender;
                var s3 = RemotingConfiguration.GetRegisteredWellKnownClientTypes();

                talk = (ILog)Activator.GetObject(typeof(ILog), s3[0].ObjectUrl);
               
                var ss = talk.Ping("Hello");
                MessageBox.Show("连接成功。\r\n" + ss);

                button1.Enabled = false;





            }
            catch (RemotingException ex)
            {
                MessageBox.Show("连接失败。" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }







        private void btnReset_Click(object sender, EventArgs e)
        {
            string[] s = CMDHelper.ExeCommand("net stop Hitearth");
            if (s[1] == "")
            {
                MessageBox.Show("关闭成功");
            }
            else
            {
                MessageBox.Show("发生错误" + "\r\n" + s[0] + "\r\n" + s[1]);
            }

            string[] s2 = CMDHelper.ExeCommand("net start Hitearth");
            if (s2[1] == "")
            {
                MessageBox.Show("开启成功");
            }
            else
            {
                MessageBox.Show("发生错误" + "\r\n" + s2[0] + "\r\n" + s2[1]);
            }
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var log = talk.GetLog();
                textBox1.Text = log;
                this.textBox1.SelectionStart = this.textBox1.Text.Length;
                this.textBox1.SelectionLength = 0;
                textBox1.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private System.Threading.Thread auto1;
        private DateTime t;
        private void button3_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (auto1 == null)
            {
                btn.Text = "停止";
                auto1 = new System.Threading.Thread(new System.Threading.ThreadStart(
                    () =>
                    {
                        while (true)
                        {
                            if (DateTime.Now > t.AddSeconds(2))
                            {
                                this.Invoke(new Action(() => this.button2_Click(null, null)));
                                t = DateTime.Now;
                            }
                        }
                    }
                ));
                auto1.Start();
            }
            else { btn.Text = "自动刷新"; auto1.Abort(); auto1 = null; }
        }









    }
}
