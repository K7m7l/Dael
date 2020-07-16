using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaelSSH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //SshClient sshclient = new SshClient("192.168.1.131", "root", "Ep#@!");
            SshClient sshclient = new SshClient("10.11.0.23", "root", "inScott!");
            sshclient.Connect();
            //SshCommand sc = sshclient.CreateCommand(richTextBox1.Text);
            //sc.Execute();
            //string answer = sc.Result;
            string cmd = string.Empty;

            this.Load += (l, d) =>
            {
                Execute(cmd, sshclient);
            };
            

            richTextBox2.KeyPress += (k, o) =>
            {
                if (o.KeyChar == (char)Keys.Return)
                {
                    string[] h = richTextBox2.Text.Split('#');
                    cmd = h[h.Length - 1];

                    Execute(cmd, sshclient);
                }
            };
        }

        public void Execute(string cmd, SshClient sshclient)
        {
            ShellStream stream = sshclient.CreateShellStream(cmd, 80, 24, 800, 600, 1024);
            richTextBox2.AppendText(sendCommand(cmd, stream).ToString());
        }

        public StringBuilder sendCommand(string customCMD, ShellStream stream)
        {
            StringBuilder answer;

            var reader = new StreamReader(stream);
            var writer = new StreamWriter(stream);
            writer.AutoFlush = true;
            WriteStream(customCMD, writer, stream);
            answer = ReadStream(reader);
            return answer;
        }

        private void WriteStream(string cmd, StreamWriter writer, ShellStream stream)
        {
            writer.WriteLine(cmd);
            while (stream.Length == 0)
            {
                Thread.Sleep(500);
            }
        }

        private StringBuilder ReadStream(StreamReader reader)
        {
            StringBuilder result = new StringBuilder();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                result.AppendLine(line);
            }
            return result;
        }
    }
}
