using System;
using System.Media;
using System.Windows.Forms;

namespace SoundPing
{
    public partial class frmSoundPing : Form
    {
        private bool _isStarted = true;
        private string _message => _isStarted ? "Is running" : "Is stopped";

        public frmSoundPing()
        {
            InitializeComponent();
        }

        private void frmSoundPing_Load(object sender, EventArgs e)
        {
            AdjustButtons();
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            _isStarted = !_isStarted;
            AdjustButtons();
        }

        private void frmSoundPing_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.BalloonTipText = _message;
                notifyIcon.ShowBalloonTip(1000);
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer($"{AppDomain.CurrentDomain.BaseDirectory}\\chimes.wav");
            simpleSound.Play();
        }

        private void AdjustButtons()
        {
            timer.Interval = Convert.ToInt32(txtPingTime.Value) * 60000;
            btnStart.Enabled = txtPingTime.Enabled = timer.Enabled = !_isStarted;
            btnStop.Enabled = timer.Enabled = _isStarted;
        }
    }
}
