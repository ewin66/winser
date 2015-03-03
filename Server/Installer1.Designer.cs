namespace Hitearth
{
    partial class Installer1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.InterProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ServiceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // InterProcessInstaller
            // 
            this.InterProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.InterProcessInstaller.Password = null;
            this.InterProcessInstaller.Username = null;
            // 
            // ServiceInstaller1
            // 
            this.ServiceInstaller1.Description = "demo";
            this.ServiceInstaller1.DisplayName = "demo";
            this.ServiceInstaller1.ServiceName = "Hitearth";
            this.ServiceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // Installer1
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ServiceInstaller1,
            this.InterProcessInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller InterProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ServiceInstaller1;
    }
}