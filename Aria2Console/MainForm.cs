using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.InteropServices;

namespace Aria2Console
{
    public partial class MainForm : Form
    {
        public class Settings
        {
            public string Aria2Path { get; set; } = string.Empty;
            public string ConfPath { get; set; } = string.Empty;
            public string AriaNgPath { get; set; } = string.Empty;
            public string WebViewPath { get; set; } = string.Empty;
            public string SessionPath { get; set; } = string.Empty;
            public void ReplaceCurrentPath(string currentPath)
            {
                Aria2Path = Aria2Path.Replace(".\\", currentPath);
                ConfPath = ConfPath.Replace(".\\", currentPath);
                AriaNgPath = AriaNgPath.Replace(".\\", currentPath);
                WebViewPath = WebViewPath.Replace(".\\", currentPath);
                SessionPath = SessionPath.Replace(".\\", currentPath);
            }
            public void Default()
            {
                Aria2Path = @".\aria2\aria2c.exe";
                ConfPath = @".\aria2\aria2.conf";
                AriaNgPath = @".\aria2\AriaNg\AriaNg-AllInOne.html";
                WebViewPath = @".\WebView.exe";
                SessionPath = @".\aria2\aria2.session";
                ReplaceCurrentPath(AppDomain.CurrentDomain.BaseDirectory);
            }
        }
        [DllImport("user32.dll", EntryPoint = "SetParent")]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        private static extern long GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern long SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);
        [DllImport("user32.dll")]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);
        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        private readonly Settings _settings = new();
        private IntPtr _aria2ConsoleHandle = IntPtr.Zero;
        private Process? _aria2Process = null;
        public MainForm()
        {
            InitializeComponent();
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            _settings.Default();
            if (!File.Exists(_settings.SessionPath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(_settings.SessionPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(_settings.SessionPath));
                }
                File.Create(_settings.SessionPath);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadAria2();
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("确定退出程序？", "退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                _aria2Process?.Kill();
                //KillAria2Progress();
                TrayNotifyIcon.Dispose();
                Environment.Exit(0);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void TrayNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Visible == true)
                {
                    Hide();
                }
                else
                {
                    Show();
                    Activate();
                }
            }
        }
        private void LoadAria2()
        {
            Stopwatch stopwatch = new();
            int getAria2Timeout = 3 * 1000;

            _aria2Process = new();
            _aria2Process.StartInfo.FileName = _settings.Aria2Path;
            _aria2Process.StartInfo.Arguments = $"--conf-path={_settings.ConfPath}";
            _aria2Process.StartInfo.WorkingDirectory = $"{Path.GetDirectoryName(_settings.Aria2Path)}";
            _aria2Process.Start();
            Debug.WriteLine(_settings.Aria2Path);
            Debug.WriteLine(_settings.ConfPath);
            stopwatch.Reset();
            stopwatch.Start();
            while (_aria2Process.MainWindowHandle == IntPtr.Zero)
            {
                System.Threading.Thread.Sleep(10);
                _aria2Process.Refresh();

                if (stopwatch.ElapsedMilliseconds > getAria2Timeout)
                {
                    stopwatch.Stop();
                    MessageBox.Show("获取aria2窗口失败", "error");
                    return;
                }
            }
            _aria2ConsoleHandle = _aria2Process.MainWindowHandle;
            /*            //屏蔽关闭按钮
                        IntPtr closeMenu = GetSystemMenu(process.MainWindowHandle, false);
                        uint SC_CLOSE = 0xF060;
                        RemoveMenu(closeMenu, SC_CLOSE, 0x0);*/
            long wndstyle = GetWindowLong(_aria2Process.MainWindowHandle, -16);
            long WS_BORDER = 0x00800000L; //去掉关闭栏
            long WS_THICKFRAME = 0x00040000L; // 去掉边框
            wndstyle &= ~WS_BORDER;
            wndstyle &= ~WS_THICKFRAME;
            SetWindowLong(_aria2Process.MainWindowHandle, -16, wndstyle);

            _ = SetParent(_aria2Process.MainWindowHandle, Handle);
            _ = SetWindowPos(_aria2Process.MainWindowHandle, 0, 0, 0, Width - 20, Height - 50, 0x0040); // 0x0001 | 0x0040
            _ = ShowWindow(_aria2Process.MainWindowHandle, 3);//最大化
            _ = ShowWindow(_aria2Process.MainWindowHandle, 1);//还原
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            _ = MoveWindow(_aria2ConsoleHandle, 0, 0, Width - 20, Height - 50, true);
        }
        /*private static void KillAria2Progress()
        {
            Process[] myProgress;
            myProgress = Process.GetProcesses();
            foreach (Process p in myProgress)
            {
                if (p.ProcessName == "aria2c")
                {
                    p.Kill();
                    return;
                }
            }
        }*/
        private bool _first_shown = true;
        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (_first_shown)
            {
                Hide();
                _first_shown = false;
            }
        }

        private void AriaNgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process process = new();
            process.StartInfo.FileName = _settings.WebViewPath;
            process.StartInfo.Arguments = _settings.AriaNgPath;
            Debug.WriteLine(_settings.AriaNgPath);
            process.Start();
        }

        private void TrayContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Drawing.Point p = Cursor.Position;
            p.Y -= TrayNotifyIcon.ContextMenuStrip?.Height ?? 27 * 4;
            TrayNotifyIcon?.ContextMenuStrip?.Show(p);
        }

        private void ReloadAria2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("确定重启aria2？可能无法保存当前状态", "重启", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                _aria2Process?.Kill();
                _aria2ConsoleHandle = IntPtr.Zero;
                LoadAria2();
            }
        }
    }
}
