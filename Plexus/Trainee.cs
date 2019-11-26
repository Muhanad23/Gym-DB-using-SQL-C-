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
    public partial class Trainee : Form
    {
        int id;
        Controller ControllerObj;
        int Tid;
        string LeaveClass;
        string classes;
        int anotherid;
        public Trainee(int id)
        {
            this.id = id;
            anotherid = id;
            ControllerObj = new Controller();
            InitializeComponent();
            Tid = id;
        }



        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            display_panel(challenges);
            DataTable dt2 = ControllerObj.Get_Challenges_info();
            comboBox1.DataSource = dt2;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Name";
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Trainee_Load(object sender, EventArgs e)
        {

        }

        //exit button
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

        //maximize button
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
            bunifuImageButton1.Location = new Point(this.Width - bunifuImageButton1.Width, 0);
            bunifuImageButton2.Location = new Point(this.Width - bunifuImageButton1.Width - bunifuImageButton2.Width, 0);
            bunifuImageButton3.Location = new Point(this.Width - bunifuImageButton1.Width - bunifuImageButton2.Width - bunifuImageButton3.Width, 0);

        }
        //make the button bright when mouse move over it (maximize icon)
        private void bunifuImageButton2_MouseMove(object sender, MouseEventArgs e)
        {
            bunifuImageButton2.BackColor = Color.FromArgb(0, 152, 254);
        }

        private void bunifuImageButton2_MouseLeave(object sender, EventArgs e)
        {
            bunifuImageButton2.BackColor = header.BackColor;
        }

        //minimize button
        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //make the button bright when mouse move over it (minimize icon)
        private void bunifuImageButton3_MouseLeave(object sender, EventArgs e)
        {
            bunifuImageButton3.BackColor = header.BackColor;
        }
        private void bunifuImageButton3_MouseMove(object sender, MouseEventArgs e)
        {
            bunifuImageButton3.BackColor = Color.FromArgb(0, 152, 254);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // side menu button
            if (SideMenu.Width == 60)
            {
                SideMenu.Visible = false;
                SideMenu.Width = 260;
                SideMenuTransition.ShowSync(logo);
                SideMenuTransition.ShowSync(SideMenu);
            }
            else
            {
                SideMenuTransition.Hide(logo);
                SideMenu.Visible = false;
                SideMenu.Width = 60;
                SideMenuTransition.ShowSync(SideMenu);
            }

        }


        //------------------------My Classes Panel---------------------//
        private void classesbutton_Click(object sender, EventArgs e)
        {
            display_panel(Classes_panel);
         
            DataTable dt = ControllerObj.ViewMyClasses(Tid);
            bunifuCustomDataGrid1.DataSource = dt;
            bunifuCustomDataGrid1.Refresh();
        }

        private void AddClass_button_Click(object sender, EventArgs e)
        {

            display_panel(Allclasses_panel);
            int branch = ControllerObj.getbranchid(Tid);
            DataTable dt = ControllerObj.ViewALLClasses(branch);
            bunifuCustomDataGrid2.DataSource = dt;
            bunifuCustomDataGrid2.Refresh();

        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            DataGridViewRow selectedRow = bunifuCustomDataGrid1.Rows[index];
            LeaveClass = selectedRow.Cells[0].Value.ToString();

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            int leave = ControllerObj.LeaveClass(Tid, LeaveClass);

            if (leave > 0)
                MessageBox.Show("Class left");
            else
                MessageBox.Show("Try Again ");
        }



        //-----------to choose which panel to appear-------//
        private void display_panel(Panel n)
        {
            Classes_panel.Visible = false;
            Allclasses_panel.Visible = false;
            Schedule_Panel.Visible = false;
            MyTrainer.Visible = false;
            Profile_panel.Visible = false;
            challenges.Visible = false;
            info_panel.Visible = false;

            if (n == Classes_panel)
                Classes_panel.Visible = true;
            else if (n == Allclasses_panel)
                Allclasses_panel.Visible = true;
            else if (n == Schedule_Panel)
                Schedule_Panel.Visible = true;
            else if (n == MyTrainer)
                MyTrainer.Visible = true;
            else if (n == Profile_panel)
                Profile_panel.Visible = true;
            else if (n == challenges)
                challenges.Visible = true;
            else if (n == info_panel)
                info_panel.Visible = true;

        }


        //-------------------------ALL Classes Panel-----------------------------//

        private void bunifuCustomDataGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            DataGridViewRow selectedRow = bunifuCustomDataGrid2.Rows[index];
            classes = selectedRow.Cells[0].Value.ToString();
        }

        private void Enroll_button_Click(object sender, EventArgs e)
        {

            int enroll = ControllerObj.EnrollinClass(Tid, classes);

            if (enroll > 0)
                MessageBox.Show("Class enrolled successfully");
            else
                MessageBox.Show("class already enrolled");
        }


        private void bunifuThinButton22_Click_1(object sender, EventArgs e)
        {


        }

        private void bunifuThinButton21_Click_1(object sender, EventArgs e)
        {

        }
        //------------------My schedule Panel-------------------//

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            display_panel(Schedule_Panel);
            DataRow Trainee = ControllerObj.Get_My_Track(Tid).Rows[0];//label
            Track_label.Text = "Track: " + Trainee["Track"];//label

            DataRow Traineee = ControllerObj.Get_My_Level(Tid).Rows[0];//label
            level_label.Text = "Level: " + Traineee["FitnessLevel"];//label

            UpdateDaysList();
            UpdateDescList();

        }

        private void UpdateDaysList()
        {
            DataTable result = ControllerObj.Days(Tid);
            Days_listbox.DisplayMember = "day";//elly hyban
            Days_listbox.DataSource = result;//ely hyban tb3 el search
            Days_listbox.Update();//execute

        }

        private void UpdateDescList()
        {
            DataTable result = ControllerObj.Description(Tid);
            Description_ListBox.DisplayMember = "Description";//elly hyban
            Description_ListBox.DataSource = result;//ely hyban tb3 el search
            Description_ListBox.Update();//execute

        }




        //--------------Profile Traineee----------------------//

        public Panel show_edit_profile(int myid, int anotheridd)
        {
            id = myid;
            anotherid = anotheridd;
            //initialize all components disabled 
            DataTable dt = ControllerObj.Get_Tracks();
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "Type";
            comboBox2.ValueMember = "Type";


            //show user's attributes
            DataRow Trainee = ControllerObj.Get_user_by_id(id).Rows[0];
            Name_lbl.Text = Edit_my_name.Text = (string)Trainee["name"];
            StartD_lbl.Text = Convert.ToDateTime(Trainee["Startdate"]).ToString("yyyy-MM-dd");
            Age_lbl.Text = Edit_my_Age.Text = Trainee["age"].ToString();
            Gender_lbl.Text = Trainee["gender"].ToString();
            My_branch_id_lbl.Text = Trainee["address"].ToString();
            Edit_my_password.Text = Trainee["password"].ToString();
            DataRow Traineee = ControllerObj.Get_Trainee(id).Rows[0];
            Mem_lbl.Text = Traineee["Membership_Length"].ToString();
            ACTIVE_lbl.Text = Traineee["Active"].ToString();
            FW_lbl.Text = Traineee["Fat_weight"].ToString();
            MW_lbl.Text = Traineee["Muscle_weight"].ToString();
            CW_lbl.Text = Traineee["current_weight"].ToString();
            SW_lbl.Text = Traineee["start_weight"].ToString();
            Height.Text = Traineee["Height"].ToString();
            Track_lbl.Text = Traineee["Track"].ToString();
            fitnesslvl_lbl.Text = Traineee["FitnessLevel"].ToString();

            if (!(string.IsNullOrEmpty((Traineee["Track"].ToString()))))
                comboBox2.SelectedValue = (Traineee["Track"].ToString());



            if (!(string.IsNullOrEmpty((Traineee["Membership_Length"].ToString()))))
                Memlen_lbl.Value = Convert.ToDecimal(Traineee["Membership_Length"].ToString());
            else
                Memlen_lbl.Value = 0;

            if (!(string.IsNullOrEmpty((Traineee["Muscle_weight"].ToString()))))
                MWNUM.Value = Convert.ToDecimal(Traineee["Muscle_weight"].ToString());
            else
                MWNUM.Value = 0;

            if (!(string.IsNullOrEmpty(Traineee["Fat_weight"].ToString())))
                FATNUM.Value = Convert.ToDecimal(Traineee["Fat_weight"]);
            else
                FATNUM.Value = 0;

            if (!(string.IsNullOrEmpty(Traineee["current_weight"].ToString())))
                CWNUM.Value = Convert.ToDecimal(Traineee["current_weight"]);
            else
                CWNUM.Value = 0;

            if (!(string.IsNullOrEmpty(Traineee["start_weight"].ToString())))
                SWNUM.Value = Convert.ToDecimal(Traineee["start_weight"]);
            else
                SWNUM.Value = 0;

            if (!(string.IsNullOrEmpty(Traineee["Height"].ToString())))
                HEIGHTNUM.Value = Convert.ToDecimal(Traineee["Height"]);
            else
                HEIGHTNUM.Value = 0;

            if (!(string.IsNullOrEmpty(Traineee["Active"].ToString())))
                ACTIVENUM.Value = Convert.ToDecimal(Traineee["Active"]);
            else
                ACTIVENUM.Value = 0;


            if (!(string.IsNullOrEmpty((Traineee["FitnessLevel"].ToString()))))
                fitnesslvl_lbll.Value = Convert.ToDecimal(Traineee["FitnessLevel"].ToString());




            DataTable db = ControllerObj.Get_all_branches();
            Edit_Branch_address.DataSource = db;
            Edit_Branch_address.DisplayMember = "address";
            Edit_Branch_address.ValueMember = "id";
            Edit_Branch_address.Text = Trainee["address"].ToString();

            //show gender of the manager on radio button
            if (Gender_lbl.Text.Equals("M"))
                Manager_male.Checked = true;
            else if (Gender_lbl.Text.Equals("F"))
                Manager_female.Checked = true;
            //select which tools to be enabled 


            if (id == anotherid)
            {
                Edit_my_name.Enabled = true;
                Edit_my_Age.Enabled = true;
                Edit_my_password.Enabled = true;
                Manager_male.Enabled = true;
                Manager_female.Enabled = true;
                Edit_Branch_address.Enabled = false;
                fitnesslvl_lbll.Enabled = true;
                ACTIVENUM.Enabled = true;
                HEIGHTNUM.Enabled = true;
                SWNUM.Enabled = true;
                CWNUM.Enabled = true;
                FATNUM.Enabled = true;
                MWNUM.Enabled = true;
                Memlen_lbl.Enabled = true;
                comboBox2.Enabled = true;
            }

            else if (anotherid < 2000000)
            {
                Edit_my_name.Enabled = false;
                Edit_my_Age.Enabled = false;
                Edit_my_password.Enabled = false;
                Manager_male.Enabled = false;
                Manager_female.Enabled = false;
                Edit_Branch_address.Enabled = true;
                fitnesslvl_lbll.Enabled = false;
                ACTIVENUM.Enabled = false;
                HEIGHTNUM.Enabled = false;
                SWNUM.Enabled = false;
                CWNUM.Enabled = false;
                FATNUM.Enabled = false;
                MWNUM.Enabled = false;
                Memlen_lbl.Enabled = true;
                comboBox2.Enabled = false;

            }

            else if (anotherid > 1999999 && anotherid < 3000000)
            {
                Edit_my_name.Enabled = false;
                Edit_my_Age.Enabled = false;
                Edit_my_password.Enabled = false;
                Manager_male.Enabled = false;
                Manager_female.Enabled = false;
                Edit_Branch_address.Enabled = false;
                fitnesslvl_lbll.Enabled = false;
                ACTIVENUM.Enabled = false;
                HEIGHTNUM.Enabled = false;
                SWNUM.Enabled = false;
                CWNUM.Enabled = false;
                FATNUM.Enabled = false;
                MWNUM.Enabled = false;
                Memlen_lbl.Enabled = false;
                comboBox2.Enabled = false;
            }

            return Profile_panel;
        }

        private void Edit_my_profile_Click(object sender, EventArgs e)
        {
            if (Edit_my_name.Text.Equals("") || Edit_my_password.Text.Equals(""))
            {
                MessageBox.Show("You must enter name and password");
                return;
            }
            string gender = null;
            if (Manager_male.Checked)
                gender = Manager_male.Text;
            else if (Manager_female.Checked)
                gender = Manager_female.Text;
            //check which profile to be updated
            
           
             int   i = id;

            int q = ControllerObj.update_trainee_info(i, Edit_my_name.Text, Edit_my_Age.Text, Edit_my_password.Text, gender, (int)Edit_Branch_address.SelectedValue,(int) Memlen_lbl.Value, (float)SWNUM.Value, (float)MWNUM.Value, (float)FATNUM.Value, (float)CWNUM.Value, (int)ACTIVENUM.Value, (int)HEIGHTNUM.Value, comboBox2.Text, (int)fitnesslvl_lbll.Value);
            if (q > 0)
            {
                MessageBox.Show("Update Succeeded");
                show_edit_profile(i,anotherid);
            }
            else
                MessageBox.Show("Update Failed");
        }



        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            show_edit_profile(this.id, this.id);
            display_panel(Profile_panel);
        }

   

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new Login();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            show_edit_profile(id, anotherid);
            display_panel(Profile_panel);
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            int p = ControllerObj.Get_my_TrainerID(id);
            if (p == 0)
                MessageBox.Show("You Don't have a Trainer yet");
            else
            {
                int tidd = ControllerObj.Get_my_TrainerID(id);
                DataRow mytrainer = ControllerObj.Get_My_Trainer(tidd).Rows[0];
                label36.Text = Edit_my_name.Text = (string)mytrainer["name"];
                label34.Text = Edit_my_Age.Text = mytrainer["age"].ToString();
                label23.Text = (string)mytrainer["gender"];

                //label20.Text = mytrainer["address"].ToString();
                display_panel(MyTrainer);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click_2(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            int p = ControllerObj.RateMyTrainer(id, Convert.ToInt32(numericUpDown1.Value));
            if (p == 1)
            {
                MessageBox.Show("you have successfully rated your trainer with " + Convert.ToInt32(numericUpDown1.Value) + " stars");
            }
            else
            {
                MessageBox.Show("you have failed to rate your trainer ");
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                DataTable dt = ControllerObj.Get_Challenge1_trainee_info();
                dataGridView1.DataSource = dt;
                dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
                dataGridView1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                DataTable dt2 = ControllerObj.Get_Challenge2_trainee_info();
                dataGridView1.DataSource = dt2;
                dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
                dataGridView1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                DataTable dt3 = ControllerObj.Get_Challenge3_trainee_info();
                dataGridView1.DataSource = dt3;
                dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
                dataGridView1.Refresh();
            }


            // hna hanget 3la 7asab el challenge ely e5tarnha w nendah el function el monasba ml controller
        }

        private void SideMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Track_label_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            display_panel(info_panel);
            int branch = ControllerObj.getbranchid(Tid);
            DataTable dt = ControllerObj.Get_Branch_info_for_Trainee(branch);
            Branch_DGV.DataSource = dt;
            Branch_DGV.Refresh();
        }

        
    }
}
