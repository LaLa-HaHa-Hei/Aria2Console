namespace Aria2Console
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            TrayContextMenuStrip = new ContextMenuStrip(components);
            reloadAria2ToolStripMenuItem = new ToolStripMenuItem();
            ariaNGToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            TrayNotifyIcon = new NotifyIcon(components);
            TrayContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // TrayContextMenuStrip
            // 
            TrayContextMenuStrip.ImageScalingSize = new Size(20, 20);
            TrayContextMenuStrip.Items.AddRange(new ToolStripItem[] { reloadAria2ToolStripMenuItem, ariaNGToolStripMenuItem, exitToolStripMenuItem });
            TrayContextMenuStrip.Name = "TrayContextMenuStrip";
            TrayContextMenuStrip.ShowImageMargin = false;
            TrayContextMenuStrip.Size = new Size(186, 104);
            TrayContextMenuStrip.Opening += TrayContextMenuStrip_Opening;
            // 
            // reloadAria2ToolStripMenuItem
            // 
            reloadAria2ToolStripMenuItem.Name = "reloadAria2ToolStripMenuItem";
            reloadAria2ToolStripMenuItem.Size = new Size(185, 24);
            reloadAria2ToolStripMenuItem.Text = "Reload Aria2";
            reloadAria2ToolStripMenuItem.Click += ReloadAria2ToolStripMenuItem_Click;
            // 
            // ariaNGToolStripMenuItem
            // 
            ariaNGToolStripMenuItem.Name = "ariaNGToolStripMenuItem";
            ariaNGToolStripMenuItem.Size = new Size(185, 24);
            ariaNGToolStripMenuItem.Text = "AriaNg";
            ariaNGToolStripMenuItem.Click += AriaNgToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(185, 24);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // TrayNotifyIcon
            // 
            TrayNotifyIcon.ContextMenuStrip = TrayContextMenuStrip;
            TrayNotifyIcon.Icon = (Icon)resources.GetObject("TrayNotifyIcon.Icon");
            TrayNotifyIcon.Visible = true;
            TrayNotifyIcon.MouseClick += TrayNotifyIcon_MouseClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(918, 461);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Aria2Consol";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            SizeChanged += MainForm_SizeChanged;
            TrayContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip TrayContextMenuStrip;
        private ToolStripMenuItem reloadAria2ToolStripMenuItem;
        private ToolStripMenuItem ariaNGToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private NotifyIcon TrayNotifyIcon;
    }
}
