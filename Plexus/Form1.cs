using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plexus
{
    public partial class Login : Form
    {
        Controller ControllerObj;
        public Login()
        {
            InitializeComponent();
            ControllerObj = new Controller();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton1_MouseMove(object sender, MouseEventArgs e)
        {
            bunifuImageButton1.BackColor = Color.Red;
        }

        private void bunifuImageButton1_MouseLeave(object sender, EventArgs e)
        {
            bunifuImageButton1.BackColor = header.BackColor;
        }


        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            //id will be written here
        }

        private void bunifuMetroTextbox2_OnValueChanged(object sender, EventArgs e)
        {
            //password will be written here
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            //log in button
            int id=-1;
            try
            {
                id = ControllerObj.CheckPassword(Convert.ToInt32(Idtextbox.Text), PassTextbox.Text);
            }
            catch(Exception) {}
            
            if (id>=0)
            {
                if (id < 2000000)
                {
                    this.Hide();
                    var form2 = new Management(id);
                    form2.Closed += (s, args) => this.Close();
                    form2.Show();
                }
                else if (id >= 3000000)
                {
                    this.Hide();
                    var form2 = new Trainee(id);
                    form2.Closed += (s, args) => this.Close();
                    form2.Show();
                }

                ///TODO : Hager -> uncomment this after creating trainer form 
                else
                {
                    this.Hide();
                    var form2 = new trainer(id);
                    form2.Closed += (s, args) => this.Close();
                    form2.Show();
                }

            }
            else
                MessageBox.Show("Wrong username or password");
        }

        
    }
}
