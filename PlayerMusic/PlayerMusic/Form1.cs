using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayerMusic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            trackBar1.Value = 50;    
        }

        String[] paths, files, directories;

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //tBoxPath.Text = Path.GetFileName(ofd.FileName);

                directories = ofd.FileNames;
                files = ofd.SafeFileNames;
                paths = ofd.FileNames;


                for (int i = 0; i < files.Length; i++)
                //for(int i = files.GetLowerBound(0); i <= files.GetUpperBound(0); ++i)
                //foreach (int i in files) 
                {
                    listBox1.Items.Add(directories[i]);
                    listBox2.Items.Add(files[i]);
                    //Array.Resize(ref files, files.Length + 1);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();
            progressBar1.Value = 0;
            label1.Text = "00:00";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.play();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
            {

                listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(listBox1.SelectedIndex>0)
            {

                listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {

                progressBar1.Maximum = (int)player.Ctlcontrols.currentItem.duration;
                progressBar1.Value = (int)player.Ctlcontrols.currentPosition;

                try
                {

                    label1.Text = player.Ctlcontrols.currentPositionString.ToString();
                    label2.Text = player.Ctlcontrols.currentItem.durationString.ToString();
                }
                catch
                {

                }
            }
      
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            player.settings.volume = trackBar1.Value;
            label3.Text = trackBar1.Value.ToString() + "%";
        }

        private void progressBar1_MouseDown(object sender, MouseEventArgs e)
        {
            player.Ctlcontrols.currentPosition = player.currentMedia.duration * e.X / progressBar1.Width;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            bool found = false;
            for (int i = 0; i <= listBox2.Items.Count - 1; i++)
            {
                if (listBox2.Items[i].ToString().ToLower().Contains(txt_searchBox.Text.ToLower()))
                {
                    listBox2.SetSelected(i, true);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                MessageBox.Show("Item not found!");
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            player.URL = paths[listBox2.SelectedIndex];
            player.Ctlcontrols.play();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            listBox1.HorizontalScrollbar = true;

            
        }
    }
}
