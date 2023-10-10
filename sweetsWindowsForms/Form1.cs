using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sweetsWindowsForms
{
    public partial class Form1 : Form
    {
        private bool drag = false;
        private Point startPoint = new Point(0, 0);
        private Form OpenedForm = null;
        private String activeForm = "";
       
        public Form1()
        {
            InitializeComponent();
            openChildForm(new Start());
        }
        private void openChildForm(Form childForm)
        {
            if (activeForm != childForm.Name)
            {
                if (OpenedForm != null)
                {
                    OpenedForm.Close();
                }
                OpenedForm = childForm;
                activeForm = childForm.Name;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                panelActive.Controls.Add(childForm);
                panelActive.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new FirmaForm());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new VlastForm());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new TovarForm());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new Tehnolog());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openChildForm(new Start());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            (new About()).ShowDialog(this);
        }

        private void panelActive_Paint(object sender, PaintEventArgs e)
        { }

    }
}
