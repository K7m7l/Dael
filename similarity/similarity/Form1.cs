using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace similarity
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int Compute(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string source = richTextBox1.Text.Trim();
            string target = richTextBox2.Text.Trim();
            double percentage;

            if ((source == null) || (target == null))
            {
                percentage = 0.0;
            }
            else if ((source.Length == 0) || (target.Length == 0))
            {
                percentage = 0.0;
            }
            else if (source == target)
            {
                percentage = 1.0;
            }
            else
            {
                int stepsToSame = Compute(source, target);
                percentage = (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
            }
            label1.Text = (percentage*100).ToString();
            label1.ForeColor = Color.Red;
        }
    }
}
