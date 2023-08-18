using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GitUserChanger.Dialogs {

    public partial class GitUserViewer : Window {

        public string email = "";
        public string userName = "";
        public string gpgkey = "";

        public GitUserViewer() {
            InitializeComponent();
            txt_Name.Text = runCli("git config --global user.name");
            txt_eMail.Text = runCli("git config --global user.email");
            txt_gpgKey.Text = runCli("git config --global user.signingkey");
        }

        string runCli(string command) {
            var process = new Process();
            var startInfo = new ProcessStartInfo {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "xcopy",
                Arguments = command,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            process.StartInfo = startInfo;
            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            // Print the output to Standard Out
            return output;
        }
    }
}
