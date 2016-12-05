namespace JinnSports.Parser.Service
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.jsonProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.parserServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // jsonProcessInstaller
            // 
            this.jsonProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.jsonProcessInstaller.Password = null;
            this.jsonProcessInstaller.Username = null;
            // 
            // parserServiceInstaller
            // 
            this.parserServiceInstaller.ServiceName = "ParserService";
            this.parserServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.jsonProcessInstaller,
            this.parserServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller jsonProcessInstaller;
        private System.ServiceProcess.ServiceInstaller parserServiceInstaller;
    }
}