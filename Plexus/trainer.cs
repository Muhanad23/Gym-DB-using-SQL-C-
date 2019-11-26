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
    public partial class trainer : Form
    {

        int id;
        int viewerid;
        Controller ControllerObj;
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
        public trainer(int id)
        {
            this.id = id;
            viewerid = id;
            ControllerObj = new Controller();
            InitializeComponent();
            show_edit_profile(id,viewerid);
            /*if (id == 1000000)
                bunifuFlatButton5.Enabled = true;*/
        }
        
        private void display_panel(Panel n)
        {
            profile.Visible = false;
            editprofil.Visible = false;
            mytrainees.Visible = false;
            classes.Visible = false;
            addclass.Visible = false;
            updateclass.Visible = false;
            removclass.Visible = false;
            equipment.Visible = false;
            editequip.Visible = false;

            if (n == profile)
                profile.Visible = true;

            else if (n == editprofil)
                editprofil.Visible = true;

            else if (n == mytrainees)
                mytrainees.Visible = true;

            else if (n == classes)
                classes.Visible = true;

            else if (n == addclass)
                addclass.Visible = true;

            else if (n == updateclass)
                updateclass.Visible = true;

            else if (n == removclass)
                removclass.Visible = true;


            else if (n == equipment)
                equipment.Visible = true;

            else if (n == editequip)
                editequip.Visible = true;
        }


            //-----------------------------------------profile start---------------------------------------------------------//
            private void loadprofile(int   id )
        {
            editedname.Enabled = false;
            editedage.Enabled = false;
            editedpass.Enabled = false;
            editedsalary.Enabled = false;
            male.Enabled = false;
            female.Enabled = false;
            editedbranch.Enabled = false;
            editedenddate.Enabled = false;
            DataRow trainer = ControllerObj.Get_user_by_id(id).Rows[0];
            name.Text = editedname.Text = (string)trainer["name"];
            startdate.Text = Convert.ToDateTime(trainer["Startdate"]).ToString("yyyy-MM-dd");
            enddate.Text = Convert.ToDateTime(trainer["Enddate"]).ToString("yyyy-MM-dd");
            editedenddate.Value = Convert.ToDateTime(trainer["Enddate"]);
            age.Text = editedage.Text = trainer["age"].ToString();
            gender.Text = (string)trainer["gender"];
            branch.Text = trainer["address"].ToString();
            salary.Text = ControllerObj.Get_trainer_salary(id).ToString();
            editedsalary.Value = Convert.ToInt32(salary.Text);
            editedsalary.Text = salary.Text;
            editedpass.Text = trainer["Password"].ToString();
            DataTable db = ControllerObj.Get_all_branches();
            editedbranch.DataSource = db;
            editedbranch.DisplayMember = "address";
            editedbranch.ValueMember = "id";
           // editedbranch.Text = trainer["address"].ToString();
            editedbranch.Show();
            //show gender of the manager on radio button
            if (gender.Text.Equals("M"))
                male.Checked = true;
            else if (gender.Text.Equals("F"))
                female.Checked = true;
        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            // the logic is load then dispaly
           // Panel p;
           show_edit_profile(id,viewerid);
            display_panel(profile);
        }

        private void show_edit_profile(int trainer_id,int viewer_id)
        {
            //select which tools to be enabled 
            loadprofile(id);
            id = trainer_id;
            viewerid = viewer_id;
            if (trainer_id == viewer_id)
            {
                editedname.Enabled = true;
                editedage.Enabled = true;
                editedpass.Enabled = true;
                male.Enabled = true;
                female.Enabled = true;
                //display_panel(profile);
            
            }
            else
            {
                editedsalary.Enabled = true;
                editedbranch.Enabled = true;
                editedenddate.Enabled = true;                
            }
        }
        private void doneeditting_Click(object sender, EventArgs e)
        {

            if (editedname.Text.Equals("") || editedpass.Text.Equals(""))
            {
                MessageBox.Show("You musht enter name and password");
                return;
            }
            string gender = null;
            if (male.Checked)
                gender = male.Text;
            else if (female.Checked)
                gender = female.Text;
            //check which profile to be updated

            int q = ControllerObj.update_Trainer(id, editedname.Text, editedage.Text, editedpass.Text, gender, (int)editedbranch.SelectedValue, editedenddate.Value, (int)editedsalary.Value);
            if (q > 0)
            {
                MessageBox.Show("Update Succeeded");
                show_edit_profile(id,viewerid);
                display_panel(profile);
            }
            else
                MessageBox.Show("Update Failed");
        }

        private void editprofile_Click(object sender, EventArgs e)
        {
            display_panel(editprofil);
        }

      

        //function to return profile panel to management
        public Panel Get_trainer_Profile(int trainer_id,int Viewer_id)
        {
            show_edit_profile(trainer_id, Viewer_id);
            return profile;
        }

        //to get edit trainer profile to management
        public Panel Get_trainer_Edit()
        {
            return editprofil;
        }

 //-----------------------------profile done--------------------------------------------------//
 //---------------------------- equipment start------------------------------------------------//

        private void loadpequipment()
        {
            DataTable db = ControllerObj.Get_All_Equipment();
            equipment_DGV.DataSource = db;
            //show equipment code in combobox to be editted
            equipcode.DataSource = db;
            equipcode.ValueMember = "Code";
            equipcode.Text = null;
            good.Checked = false;
            damage.Checked = false;

        }
            private void edditequip_button_Click(object sender, EventArgs e)
        {
            display_panel(editequip);
        }

        private void doneequipeditting_Click(object sender, EventArgs e)
        {
            string status = null;
            if (good.Checked)
                status = "good";
            else if (damage.Checked)
                status = "Damaged";
            if (equipcode.SelectedItem == null)
                
                MessageBox.Show("please enter the code");
            else
            {
                int p = ControllerObj.update_equipment((int)equipcode.SelectedValue, status);
                if (p > 0)
                {
                    MessageBox.Show("Equipment inserted successfully");
                    loadpequipment();
                    display_panel(equipment);
                }
                else
                    MessageBox.Show("Insertion failed"); }
        }

        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            loadpequipment();
            display_panel(equipment);
        }
//----------------------------------equipment done------------------------------------------------------------------------//
//----------------------------------my trainees start---------------------------------------------------------------------//

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            loadtrainees(id);
            display_panel(mytrainees);
            // no one shold be able to view the trainer but the head manager
        }
        private void loadtrainees(int id)
        {
            
            float total_rating = 0;
            int count = 0;
            float Rating = 0;
            DataTable db = ControllerObj.Get_MY_Trainees(id);
            employeeListBox.DisplayMember = "Name";
            employeeListBox.ValueMember = "ID";
            employeeListBox.DataSource = db;
            // employeeListBox.Update();
            count = ControllerObj.GetRatingCount(id);
            total_rating = ControllerObj.GetRating(id);
            if (count!=0)
                Rating = total_rating / count;
            Rating_count.Text = count.ToString();
            rating.Text = Rating.ToString();
            //show trainee name in combobox to be deleted
        }
        private void employeeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
          int i;
            i =(int)employeeListBox.SelectedValue; ;
            /* show_edit_profile(i);  
           -should be replaced by a special show function for the trainee
            as he has extra attributes.
            -you should add some code in this func to check for the trainer assigned for this trainee with id(i),
            if he is the trianer trying to access this trainee with id i, then show him the profile and 
            enable him to edit the training schdule, this would be done by :
            1- get the trainer id of the trainee with id (i) through a query to the db
            2- if it does equall  (this.id) , then call function show panel(trainee profile) and enable 
            the updating in the training schdule 
             */
            // *** display_panel(profile);****
            /* i will not call this function here  as supposed so please do not forget to call
              it in your part 3shan mtbwzleesh el donia :D
                 */

        }
        //------------------------------------my trainee done-----------------------------------//
        //-----------------------------------classes start--------------------------------------//
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            loadclasses();
            display_panel(classes);

        }
        private void loadclasses()
        {
            
     
            string[] dayss = { "sunday","monday","tuesday","wednesday","thursday","friday","saturday"};
  
            classnames.DisplayMember = classnames1.DisplayMember = "Name";
            classnames.ValueMember = classnames1.ValueMember = "Name";
            DataTable db = ControllerObj.Get_All_Classes();
            eclasses.DataSource = db;
            classnames.DataSource = classnames1.DataSource= db;
            instructors.DisplayMember = instructors1.DisplayMember = "Name";
            instructors.ValueMember = instructors1.ValueMember = "ID";
            DataTable db2 = ControllerObj.GetTrainers();
            instructors.DataSource = instructors1.DataSource = db2;
            days.DataSource = days1.DataSource = dayss;
            ////////////////////////////////////////////
            classnames1.Text = null;
            start1.Text = null;
            end1.Text = null;
            days1.Text = null;
            description1.Text = null;
            instructors1.Text = null;
            /////////////////////////////////////////////////////////
            classname.Text = null;
            start.Text = null;
            end.Text = null;
            days.Text = null;
            description.Text = null;
            instructors.Text = null;
           
         
        }
        private void add_Click(object sender, EventArgs e)
        {

            int p = ControllerObj.insert_new_Class(classname.Text, days.Text, start.Text, end.Text,description.Text,instructors.SelectedValue);

            if (p > 0)
            {
                MessageBox.Show("A new class With (name=" + classname.Text + ") " + " has been inserted successfully");
                loadclasses();
                display_panel(classes);
            }
            else
                MessageBox.Show("Insertion failed");

        }

        private void remove_Click_1(object sender, EventArgs e)
        {
            string CName = classnames.Text;
            int p = ControllerObj.Remove_Class(classnames.Text);
            
            if (p > 0)
            {
                MessageBox.Show(" class With (name=" + CName + " ) " + " has been deleted successfully");
                loadclasses();
                display_panel(classes);
            }
            else
                MessageBox.Show("deletion failed");

        }
       
        private void addnclass_Click(object sender, EventArgs e)
        {

            display_panel(addclass); // the panel is ready as it has been loaded in the loader

        }
        private void updatenclass_Click(object sender, EventArgs e)
        {
            display_panel(updateclass);// the panel is ready as it has been loaded in the loader
        }

        private void removeclass_Click(object sender, EventArgs e)
        {
            display_panel(removclass);// the panel is ready as it has been loaded in the loader
        }
       /* public void loadupdateclass()
         {
              selctedclass = ControllerObj.Get_class_by_name((classnames1.ValueMember).ToString()).Rows[0];
             days1.Text = (string)selctedclass["Day"];
             start1.Text = (string)selctedclass["start_time"];
             end1.Text = (string)selctedclass["end_time"];
             description1.Text = (string)selctedclass["description"];
             instructors1.Text = (string)selctedclass["trainer_id"];

         */
/*private void classnames1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadupdateclass();
        }*/
         
        private void update_Click_1(object sender, EventArgs e)
        {
            int p = ControllerObj.Update_Class(classnames1.Text, days1.Text, start1.Text, end1.Text, description1.Text, instructors1.SelectedValue);

            if (p > 0)
            {
                MessageBox.Show(" class With (name=" + classnames1.Text + " ) " + " has been updated successfully");
                loadclasses();
                display_panel(classes);
            }
            else
                MessageBox.Show("updating failed");
        }
        //----------------------------------------classes done---------------------------------------------------------------------------------//


        

       

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            //log out button
            this.Hide();
            var form2 = new Login();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        
    }
}
