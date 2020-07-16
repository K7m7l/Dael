using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaelSSH
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Renci.SshNet.SshClient sshClient = new Renci.SshNet.SshClient("10.11.0.23", "root", "inScott!");
            sshClient.Connect();
            var command = sshClient.RunCommand("top");

            var line = command.Result.Split('\n');

            for (int i = 3; i < line.Length - 1; i++)
            {
                var li = line[i];
                var words = li.Split(' ');
                List<string> fillterwords = new List<string>();

                foreach (var w in words)
                {
                    if (w != "")
                    {
                        fillterwords.Add(w);
                    }
                }
            }

        }
    }
}
