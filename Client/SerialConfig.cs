using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hitearth
{
    public partial class SerialConfig : Form
    {
        public SerialConfig(string path)
        {
            InitializeComponent();
            this.path = path;
        }
        private string path;
        private void SerialConfig_Load(object sender, EventArgs e)
        {
            var s = System.Xml.Linq.XDocument.Load(path);
            textBox1.Text = s.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var s = System.Xml.Linq.XDocument.Parse(textBox1.Text);
                s.Save(path);
                MessageBox.Show("保存成功！要重启采集服务，配置才会生效");
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
