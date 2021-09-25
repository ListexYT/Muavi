using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using System.Xml.Linq;

namespace Muavi
{
    public partial class Form1 : Form
    {
        int i = 0; long j = 0;
        ArrayList files = new ArrayList();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.

            timer1.Start();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        private void showSubMenu(System.Windows.Forms.Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void btnMedia_Click(object sender, EventArgs e)
        {
            showSubMenu(panelMediaSubMenu);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ofd.Multiselect = true;
            ofd.Filter = "Ауди файлы(*.mp3)| *.mp3";
            ofd.ShowDialog();

            foreach (string dir in ofd.FileNames)
            {
                files.Add(dir);
                listBox1.Items.Add(dir);

            }
            foreach (string track in ofd.SafeFileNames)
            {
                listBox2.Items.Add(track);
            }

        }
        
        private void play_click(object sender, EventArgs e)
        {
            timer1.Start();
            pause.Visible = true;
            string s;
            listBox1.SelectedIndex = listBox2.SelectedIndex;
            s = Convert.ToString(listBox1.SelectedItem);
            if (axWindowsMediaPlayer1.URL == "" || axWindowsMediaPlayer1.URL != s)
                axWindowsMediaPlayer1.URL = s;
            else
                axWindowsMediaPlayer1.Ctlcontrols.play();

            play.Visible = false;
            trackBar1.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void next_click(object sender, EventArgs e)
        {
            try
            {
                listBox1.SelectedIndex = listBox1.SelectedIndex + 1;


                listBox2.SelectedIndex = listBox1.SelectedIndex;
                axWindowsMediaPlayer1.URL = Convert.ToString(listBox1.SelectedItem);
            }
            catch (Exception ex)
            {
                pause.Visible = false;
                play.Visible = true;

            }
        }

        private void prev_click(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
            listBox2.SelectedIndex = listBox1.SelectedIndex;

            axWindowsMediaPlayer1.URL = Convert.ToString(listBox1.SelectedItem);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pause_click(object sender, EventArgs e)
        {
            pause.Visible = false;
            play.Visible = true;
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                next_click(sender, e);
            }
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {

                trackBar1.Maximum = (int)axWindowsMediaPlayer1.currentMedia.duration + 1;
                trackBar1.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;

            }
            label2.Text = Convert.ToString(trackBar1.Value / 60) + ":" + Convert.ToString(trackBar1.Value % 60);
            label1.Text = Convert.ToString(trackBar1.Maximum / 60) + ":" + Convert.ToString(trackBar1.Maximum % 60 - 1);

            j++;
        }




        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar1.Value;
        }

        private void mc(object sender, MouseEventArgs e)
        {
            play_click(sender, e);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = trackBar2.Value;
            label3.Text = Convert.ToString(trackBar2.Value)+"%";
        }

        private void playList(object sender, EventArgs e)
        {
            if (panel1.Visible == true)
            {
                panel1.Visible = false;
            }
            else
            {
                panel1.Visible = true;
            }
        }



        private void replay_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.setMode("loop", false);
            pictureBox2.Visible = true;
            pictureBox5.Visible = false;
            }

        private void playss_click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            pictureBox2.Visible = false;
            pictureBox5.Visible = true;
        }


    }
    }