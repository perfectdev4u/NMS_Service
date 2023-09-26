using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NmsService
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        HesService service = new HesService();
        private void btnStart_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            service.StartServiceFromUI();
            btnStop.Enabled = true;
            btnStart.Enabled = false;
            lblStatus.Text = "Started";
            Cursor.Current = Cursors.Default;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopService();
        }
        private void StopService()
        {
            Cursor.Current = Cursors.WaitCursor;
            service.StopServiceFromUI();
            lblStatus.Text = "Stopped";
            Application.DoEvents();
            btnStop.Enabled = false;
            btnStart.Enabled = true;

            Cursor.Current = Cursors.Default;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to exit?", "confirm", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes)
            {
                return;
            }
            StopService();
            Application.Exit();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopService();
            Application.Exit();
        }
    }
}
