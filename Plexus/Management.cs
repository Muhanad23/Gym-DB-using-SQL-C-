using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Framework.UI;

namespace Plexus
{
    public partial class Management : Form
    {
        int id;
        Controller ControllerObj;
        int index1 = -1;
        int index2 = -1;
        trainer trainerobj;
        Trainee traineeobj;

        public Management(int id)
        {
            this.id = id;
            ControllerObj = new Controller();
            InitializeComponent();
            if (id == 1000000)
                bunifuFlatButton5.Enabled = true;
            
        }

        //-------------------------------buttons view----------------------------------
        //for exit button
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

        //maximize the window
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

        //make the button bright when mouse move over it (minimize icon)
        private void bunifuImageButton3_MouseLeave(object sender, EventArgs e)
        {
            bunifuImageButton3.BackColor = header.BackColor;
        }
        private void bunifuImageButton3_MouseMove(object sender, MouseEventArgs e)
        {
            bunifuImageButton3.BackColor = Color.FromArgb(0, 152, 254);
        }
        //minimize the window on clicking minimize icon
        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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
        //------------------------end of button view----------------------------------



       
        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            //log out button
            this.Hide();
            var form2 = new Login();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }



        //-------------------------------------------profile panel-----------------------------------------------

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            //profile button
            show_edit_profile(id);
            display_panel(Profile_panel);
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
            int i;
            if (Management_combobox.SelectedValue == null)
                i = id;
            else i = (int)Management_combobox.SelectedValue;
            int q = ControllerObj.update_manager(i, Edit_my_name.Text, Edit_my_Age.Text, Edit_my_password.Text, gender, (int)Edit_Branch_address.SelectedValue, Edit_my_end_date.Value,(int)Edit_salary.Value);
            if (q > 0)
            {
                MessageBox.Show("Update Succeeded");
                show_edit_profile(i);
            }
            else
                MessageBox.Show("Update Failed");
        }


        //----------------------------------------------end of profile panel----------------------------------------------------



        //----------------------------------------------------equipment panel--------------------------------------------------

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            //Equipment button
            //show all equipment
            DataTable db = ControllerObj.Get_All_Equipment();
            equipment_DGV.DataSource = db;
            //show equipment code in combobox to be deleted
            Equipment_combobox.DataSource = db;
            Equipment_combobox.ValueMember = "Code";
            Equipment_combobox.Text = null;
            Add_Equipment_name.Text = null;
            Add_Equipment_Code.Value = ControllerObj.Get_new_equipment_PK();
            db = ControllerObj.Get_all_branches();
            comboBox2.DataSource = db;
            comboBox2.DisplayMember = "address";
            comboBox2.ValueMember = "id";
            display_panel(Equipment_Panel);
        }

        //delete equipment button
        private void Remove_equpment_button_Click(object sender, EventArgs e)
        {

            int p = ControllerObj.delete_equipment(Equipment_combobox.SelectedValue);
            if (p == 0)
                MessageBox.Show("Deletion failed");
            else
            {
                MessageBox.Show("Equipment deleted successfully");
                bunifuFlatButton2_Click(sender, e);
            }
        }


        //insert equipment button
        private void Add_equipment_button_Click(object sender, EventArgs e)
        {
            string status = null;
            if (Good_radio_button.Checked)
                status = Good_radio_button.Text;
            else if (Damaged_radio_button.Checked)
                status = Damaged_radio_button.Text;
            int p = ControllerObj.insert_equipment((int)Add_Equipment_Code.Value, Add_Equipment_name.Text, status, (int)comboBox2.SelectedValue);
            if (p > 0)
            {
                MessageBox.Show("Equipment inserted successfully");
                bunifuFlatButton2_Click(sender, e);
            }
            else
                MessageBox.Show("Insertion failed");
        }

        //---------------------------------------------end of equipment panel----------------------------------------------------



        //-------------------------------------------------Trainer panel---------------------------------------------------------

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            //Trainer button
            //show all trainer
            DataTable db = ControllerObj.Get_All_Trainers();
            Trainer_DGV.DataSource = db;
            //show trainer name in combobox to be deleted
            Trainer_combobox.DataSource = db;
            Trainer_combobox.DisplayMember = "name";
            Trainer_combobox.ValueMember = "id";
            Trainer_combobox.Text = null;
            Add_Trainer_name.Text = null;
            db = ControllerObj.Get_all_branches();
            comboBox1.DataSource = db;
            comboBox1.DisplayMember = "address";
            comboBox1.ValueMember = "id";
            //Generate P_K for new trainer
            Add_Trainer_id.Value = ControllerObj.Get_new_Trainer_PK();
            display_panel(Trainer_panel);
        }

        //add trainer button
        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if (Add_Trainer_name.Text.Equals(""))
            {
                MessageBox.Show("you must enter name ");
                return;
            }
            string gender = null;
            if (Add_Male_Trainer.Checked)
                gender = Add_Male_Trainer.Text;
            else if (Add_Female_trainer.Checked)
                gender = Add_Female_trainer.Text;

            int p = ControllerObj.insert_new_trainer((int)Add_Trainer_id.Value, Add_Trainer_name.Text, gender, Add_Trainer_start_date.Value, Add_Trainer_end_date.Value, (int)comboBox1.SelectedValue, (int)Add_Trainer_Salary.Value);

            if (p > 0)
            {
                MessageBox.Show("A new Trainer With (id=" + (int)Add_Trainer_id.Value + " , And password= '12345') " + " inserted successfully");
                bunifuFlatButton3_Click(sender, e);
            }
            else
                MessageBox.Show("Insertion failed");
        }

        //remove trainer button
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (Trainer_combobox.SelectedItem == null)
            {
                MessageBox.Show("You must select trainer to be deleted");
                return;
            }

            int p = ControllerObj.delete_trainer((int)Trainer_combobox.SelectedValue);

            if (p > 0)
            {
                MessageBox.Show("Trainer deleted successfully");
                bunifuFlatButton3_Click(sender, e);
            }
            else
                MessageBox.Show("deletion failed");
        }

        //edit/view trainer button
        private void Edit_Trainer_Click(object sender, EventArgs e)
        {
            display_panel(null);
            if (Trainer_combobox.SelectedItem == null)
            {
                display_panel(Trainer_panel);
                MessageBox.Show("You must select trainer to be able to Edit or view");
                
                return;
            }
            
            trainerobj = new trainer((int)Trainer_combobox.SelectedValue);
            //display trainer profile
            Panel trainer_profile = trainerobj.Get_trainer_Profile((int)Trainer_combobox.SelectedValue, id);
            this.Controls.Add(trainer_profile);
            trainer_profile.BringToFront();
            trainer_profile.Visible=true;
            //display trainer edit profile
            Panel trainer_edit = trainerobj.Get_trainer_Edit();
            this.Controls.Add(trainer_edit);
            trainer_edit.BringToFront();
            index2 = this.Controls.IndexOf(trainer_edit);            
            index1 = this.Controls.IndexOf(trainer_profile);
        }
        //--------------------------------------------end of Trainer panel----------------------------------------------------




        //------------------------------------------------------Trainee panel-------------------------------------------------

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            //Trainees button
            //show all trainees
            DataTable db = ControllerObj.Get_All_Trainees();
            Trainee_DGV.DataSource = db;
            //show trainee name in combobox to be deleted
            Trainee_combobox.DataSource = db;
            Trainee_combobox.DisplayMember = "name";
            Trainee_combobox.ValueMember = "id";
            Trainee_combobox.Text = null;
            Add_Trainee_name.Text = null;
            db = ControllerObj.Get_all_branches();
            comboBox3.DataSource = db;
            comboBox3.DisplayMember = "address";
            comboBox3.ValueMember = "id";
            //generate P_K for new trainee
            Add_Trainee_id.Value = ControllerObj.Get_new_Trainee_PK();
            display_panel(Trainee_panel);
        }



        //add trainee button
        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            if (Add_Trainee_name.Text.Equals(""))
            {
                MessageBox.Show("you must enter name ");
                return;
            }
            string gender = null;
            if (Add_Male_Trainee.Checked)
                gender = Add_Male_Trainee.Text;
            else if (Add_Female_Trainee.Checked)
                gender = Add_Female_Trainee.Text;

            int p = ControllerObj.insert_new_trainee((int)Add_Trainee_id.Value, Add_Trainee_name.Text, gender, Add_Trainee_start_date.Value, (int)numericUpDown2.Value, (int)comboBox3.SelectedValue);

            if (p > 0)
            {
                MessageBox.Show("A new Trainee With (id=" + (int)Add_Trainee_id.Value + " , And password= '12345') " + " inserted successfully");
                bunifuFlatButton4_Click(sender, e);
            }
            else
                MessageBox.Show("Insertion failed");
        }


        //remove trainee button
        private void Remove_trainee_button_Click(object sender, EventArgs e)
        {

            if (Trainee_combobox.SelectedItem == null)
            {
                MessageBox.Show("You must select trainee to be deleted");
                return;
            }

            int p = ControllerObj.delete_trainee((int)Trainee_combobox.SelectedValue);

            if (p > 0)
            {
                MessageBox.Show("Trainee deleted successfully");
                bunifuFlatButton4_Click(sender, e);
            }
            else
                MessageBox.Show("deletion failed");
        }

        //edit or view trainee profile
        private void Edit_View_trainee_Click(object sender, EventArgs e)
        {
            display_panel(null);
            if (Trainee_combobox.SelectedItem == null)
            {
                display_panel(Trainee_panel);
                MessageBox.Show("You must select trainer to be able to Edit or view");
                return;
            }

            traineeobj = new Trainee((int)Trainee_combobox.SelectedValue);
            //display trainee profile
            Panel trainee_profile = traineeobj.show_edit_profile((int)Trainee_combobox.SelectedValue, id);
            this.Controls.Add(trainee_profile);
            trainee_profile.BringToFront();
            trainee_profile.Visible = true;
            index1 = this.Controls.IndexOf(trainee_profile);
        }

        //----------------------------------------end of Trainee panel--------------------------------------------------


        //-----------------------------------------Management Panel-----------------------------------------------------

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            if (id != 1000000)
                return;
            //management button
            //show all managers
            DataTable db = ControllerObj.Get_All_Managers();
            Management_DGV.DataSource = db;
            //show manager name in combobox to be deleted
            Management_combobox.DataSource = db;
            Management_combobox.DisplayMember = "name";
            Management_combobox.ValueMember = "id";
            Management_combobox.Text = null;
            Add_new_manager_name.Text = null;
            db = ControllerObj.Get_all_branches();
            comboBox4.DataSource = db;
            comboBox4.DisplayMember = "address";
            comboBox4.ValueMember = "id";
            //Generate P_K for new manager
            Add_Manager_id.Value = ControllerObj.Get_new_Manager_PK();
            display_panel(Management_panel);
        }


        //add manager button
        private void Add_new_manager_button_Click(object sender, EventArgs e)
        {
            if (Add_new_manager_name.Text.Equals(""))
            {
                MessageBox.Show("you must enter name ");
                return;
            }
            string gender = null;
            if (Add_new_male_manager.Checked)
                gender = Add_new_male_manager.Text;
            else if (Add_new_female_manager.Checked)
                gender = Add_new_female_manager.Text;

            int p = ControllerObj.insert_new_manager((int)Add_Manager_id.Value, Add_new_manager_name.Text, gender, Add_new_manager_start_date.Value, Add_new_manager_end_date.Value, (int)comboBox4.SelectedValue, (int)Add_new_manager_salary.Value);

            if (p > 0)
            {
                MessageBox.Show("A new manager With (id=" + (int)Add_Manager_id.Value + " , And password= '12345') " + " inserted successfully");
                bunifuFlatButton5_Click(sender, e);
            }
            else
                MessageBox.Show("Insertion failed");
        }

        //remove button
        private void Remove_manager_button_Click(object sender, EventArgs e)
        {
            if (Management_combobox.SelectedValue == null)
            {
                MessageBox.Show("You must select Manager to be deleted");
                return;
            }
            if((int)Management_combobox.SelectedValue==1000000)
            {
                MessageBox.Show("This user can't be deleted");
                return;
            }

            int p = ControllerObj.delete_manager((int)Management_combobox.SelectedValue);

            if (p > 0)
            {
                MessageBox.Show("Manager deleted successfully");
                bunifuFlatButton5_Click(sender, e);
            }
            else
                MessageBox.Show("deletion failed");

        }

        //edit/view button
        private void Edit_view_manager_button_Click(object sender, EventArgs e)
        {
            if (Management_combobox.SelectedItem == null)
            {
                MessageBox.Show("You must choose manager to be edited");
                return;
            }
            display_panel(Profile_panel);
            show_edit_profile((int)Management_combobox.SelectedValue);
            
        }

        //--------------------------------------end Management panel----------------------------------------------------



        //---------------------------------------------Branch panel-----------------------------------------------------

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            //make only the big boss to change branches
            if (id == 1000000)
            {
                Display_add_branch.Visible = true;
                Display_branch_editing.Visible = true;
            }
            else
            {
                Display_add_branch.Visible = false;
                Display_branch_editing.Visible = false;
            }
            //Branch info button
            //show all branches
            DataTable db = ControllerObj.Get_All_Branches();
            Branch_DGV.DataSource = db;
            //show manager name in combobox to be deleted
            branch_id.DataSource = db;
            branch_id.DisplayMember = "id";
            branch_id.ValueMember = "id";
            //branch_id.Text = null;
            branch_id.SelectedItem = null;
            db = ControllerObj.Get_All_Managers();
            Add_branch_manager.DataSource = db;
            Add_branch_manager.DisplayMember = "name";
            Add_branch_manager.ValueMember = "id";
            Add_branch_manager.Text = null;
            Edit_branch_manager.DataSource = db;
            Edit_branch_manager.DisplayMember = "name";
            Edit_branch_manager.ValueMember = "id";
            Edit_branch_manager.Text = null;
            //Generate P_K for new branch
            numericUpDown1.Value = ControllerObj.Get_new_Branch_PK();
            display_panel(Branch_panel);
        }

        //add new branch button
        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            int p = ControllerObj.insert_new_branch((int)numericUpDown1.Value, Add_branch_address.Text, (int?)Add_branch_manager.SelectedValue);

            if (p > 0)
            {
                MessageBox.Show("branch inserted successfully");
                bunifuFlatButton6_Click(sender, e);
            }
            else
                MessageBox.Show("Insertion failed");
        }

        //edit branch button
        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            if (branch_id.SelectedItem == null)
            {
                MessageBox.Show("You must choose branch to be edited");
                return;
            }
            int p = ControllerObj.Edit_branch((int)branch_id.SelectedValue, Update_branch_address.Text, (int?)Edit_branch_manager.SelectedValue);
            if (p > 0)
            {
                MessageBox.Show("Branch edited successfully");
                bunifuFlatButton6_Click(sender, e);
            }
            else
                MessageBox.Show("Update failed");
        }

        //remove branch button
        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            if (branch_id.SelectedItem == null)
            {
                MessageBox.Show("You must choose branch to be deleted");
                return;
            }
            int p = ControllerObj.Delete_branch((int)branch_id.SelectedValue);
            if (p > 0)
            {
                MessageBox.Show("Branch deleted successfully");
                bunifuFlatButton6_Click(sender, e);
            }
            else
                MessageBox.Show("deletion failed");
        }

        //to get branch info to be edited
        private void branch_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (branch_id.SelectedItem == null)
                return;
            DataRow db = ControllerObj.Get_Branch_info((int)branch_id.SelectedValue).Rows[0];
            Edit_branch_manager.SelectedValue = db["manager"];
            Update_branch_address.Text = db["address"].ToString();
        }
        //------------------------------------------end of branch panel-------------------------------------------------

        //-------------------------------------------challenges---------------------------------------------------------
        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            DataTable db = ControllerObj.Get_All_Challenges();
            Challenges_combobox.DataSource = db;
            Challenges_combobox.ValueMember = "name";
            Challenges_combobox.DisplayMember = "name";
            Challenges_combobox.SelectedItem = null;
            Challenge_name.Text = null;
            Challenge_Description.Text = null;
            display_panel(Challenges_Panel);
        }

        private void Challenges_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Challenges_combobox.SelectedItem == null)
                return;
            DataRow db = ControllerObj.Get_Challenges(Challenges_combobox.SelectedValue.ToString()).Rows[0];
            Challenge_name.Text = db["name"].ToString();
            Challenge_Description.Text = db["info"].ToString();
        }

        private void bunifuThinButton28_Click(object sender, EventArgs e)
        {
            if (Challenges_combobox.SelectedItem == null)
            {
                MessageBox.Show("You must choose Challenge to be Updated");
                return;
            }
            int p=ControllerObj.Edit_challenge(Challenges_combobox.SelectedValue.ToString(), Challenge_name.Text, Challenge_Description.Text);
            if (p > 0)
            {
                MessageBox.Show("Challenge Updated successfully");
                bunifuFlatButton9_Click(sender, e);
            }
            else
                MessageBox.Show("Updating failed");
        }
        //-----------------------------------------end of challenges panel----------------------------------------------

        //------------------------------------------------statistics----------------------------------------------------
        private void Management_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.Trainee' table. You can move, or remove it, as needed.
            this.TraineeTableAdapter.Fill(this.DataSet1.Trainee);
            // TODO: This line of code loads data into the 'DataSet1.Class_Trainee' table. You can move, or remove it, as needed.
            this.Class_TraineeTableAdapter.Fill(this.DataSet1.Class_Trainee);

            this.reportViewer1.RefreshReport();
            this.reportViewer2.RefreshReport();
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            display_panel(statistics);
        }

        //----------------------------------------------------------------------------------------------------------------



        //function to choose which panel to be displayed 
        private void display_panel(Panel n)
        {

            check_index();
            Profile_panel.Visible = false;
            Equipment_Panel.Visible = false;
            Trainer_panel.Visible = false;
            Trainee_panel.Visible = false;
            Management_panel.Visible = false;
            Branch_panel.Visible = false;
            Challenges_Panel.Visible = false;
            statistics.Visible = false;

            if (n == Profile_panel)
                Profile_panel.Visible = true;
            else if (n == Equipment_Panel)
                Equipment_Panel.Visible = true;
            else if (n == Trainer_panel)
                Trainer_panel.Visible = true;
            else if (n == Trainee_panel)
                Trainee_panel.Visible = true;
            else if (n == Management_panel)
                Management_panel.Visible = true;
            else if (n == Branch_panel)
                Branch_panel.Visible = true;
            else if (n == Challenges_Panel)
                Challenges_Panel.Visible = true;
            else if (n == statistics)
                statistics.Visible = true;
        }


        //function to display profile with special conditons
        private void show_edit_profile(int id)
        {
            

            //initialize all components disabled 
            Edit_my_name.Enabled = false;
            Edit_my_Age.Enabled = false;
            Edit_my_password.Enabled = false;
            Edit_salary.Enabled = false;
            Manager_male.Enabled = false;
            Manager_female.Enabled = false;
            Edit_Branch_address.Enabled = false;
            Edit_my_end_date.Enabled = false;

            //show user's attributes
            DataRow manager = ControllerObj.Get_user_by_id(id).Rows[0];
            Name_lbl.Text = Edit_my_name.Text = (string)manager["name"];
            StartD_lbl.Text = Convert.ToDateTime(manager["Startdate"]).ToString("yyyy-MM-dd");
            EndD_lbl.Text = Convert.ToDateTime(manager["Enddate"]).ToString("yyyy-MM-dd");
            Edit_my_end_date.Value = Convert.ToDateTime(manager["Enddate"]);
            Age_lbl.Text = Edit_my_Age.Text = manager["age"].ToString();
            Gender_lbl.Text = (string)manager["gender"];
            My_branch_id_lbl.Text = manager["address"].ToString();
            Salary_lbl.Text = ControllerObj.Get_manager_salary(id).ToString();
            Edit_salary.Value = Convert.ToInt32(Salary_lbl.Text);
            Edit_my_password.Text = manager["password"].ToString();
            DataTable db = ControllerObj.Get_all_branches();
            Edit_Branch_address.DataSource = db;
            Edit_Branch_address.DisplayMember = "address";
            Edit_Branch_address.ValueMember = "id";
            Edit_Branch_address.Text = manager["address"].ToString();

            //show gender of the manager on radio button
            if (Gender_lbl.Text.Equals("M"))
                Manager_male.Checked = true;
            else if (Gender_lbl.Text.Equals("F"))
                Manager_female.Checked = true;
            //select which tools to be enabled 
            if (this.id==id)
            {
                Edit_my_name.Enabled = true;
                Edit_my_Age.Enabled = true;
                Edit_my_password.Enabled = true;
                Manager_male.Enabled = true;
                Manager_female.Enabled = true;
                //this line to be added here in the final version
                Management_combobox.Text = null;
            }
            if (this.id==1000000)
            {
                Edit_salary.Enabled = true;
                Edit_Branch_address.Enabled = true;
                Edit_my_end_date.Enabled = true;
            }
        }

        private void check_index()
        {
            if (index1 != -1)
            {
                this.Controls.Remove(this.Controls[index1]);
                index1 = -1;
            }
            if (index2 != -1)
            {
                this.Controls.Remove(this.Controls[index2]);
                index2 = -1;
            }
        }

        
    }
}
