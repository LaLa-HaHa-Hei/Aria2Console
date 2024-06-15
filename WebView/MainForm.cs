using System.Text;

namespace WebView
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            if (Environment.GetCommandLineArgs().Length <= 1)
            {
                Environment.Exit(0);
            }
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Environment.GetCommandLineArgs().Length == 2)
            {
                webView2.Source = new Uri(Environment.GetCommandLineArgs()[1].Replace("\\", "/"));
            }
            else
            {
                StringBuilder sb = new(Environment.GetCommandLineArgs()[0]);
                for (int i = 1; i < Environment.GetCommandLineArgs().Length; i++)
                {
                    sb.Append(' ');
                    sb.Append(Environment.GetCommandLineArgs()[i]);
                }
                webView2.Source = new Uri(sb.ToString().Replace("\\", "/"));
            }
        }
    }
}
