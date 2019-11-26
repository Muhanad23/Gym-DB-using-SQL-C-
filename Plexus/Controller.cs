using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Plexus
{
    public class Controller
    {
        private DBManager dbMan; // A Reference of type DBManager 
        // (Initially NULL; NO DBManager Object is created yet)

        public Controller()
        {
            dbMan = new DBManager(); // Create the DBManager Object
        }

        public int CheckPassword(int id, string password)
        {
            //Query the DB to check for id/password
            string query = "SELECT id from Users where id = " + id + " and password='" + password + "';";
            object p = dbMan.ExecuteScalar(query);
            if (p == null) return -1;
            else return (int)p;
        }

        //------------------------------------------------[Muhanad]:Management queries ---------------------------------------------------------------------

        //select all info about user
        public DataTable Get_user_by_id(int id)
        {
            string query = "Select u.*,b.address from users u,gym_branch b where u.branch_id=b.id and u.id = " + id + ";";
            return dbMan.ExecuteReader(query);
        }

        //select all branches
        public DataTable Get_all_branches()
        {
            string query = "select * from gym_branch ;";
            return dbMan.ExecuteReader(query);
        }

        //select salary of manager
        public int Get_manager_salary(int id)
        {
            string query = "Select salary from management where id = " + id + ";";
            object p = dbMan.ExecuteScalar(query);
            if (p == null) return 0;
            else return (int)p;
        }

        //update user's name,age,password with id
        public int update_manager(int id, object name, object age, object password, string gender, int branch_id, DateTime end_date, int salary)
        {
            object c;
            if ((string)age == "")
                c = "NULL";
            else c = Convert.ToInt32(age);
            string query = "Update Users Set " + "age=" + c + " ,name='" + name + "' ,password='" + password + "',gender='" + gender + "',branch_id=" + branch_id + ",Enddate='" + end_date.ToString("yyyy-MM-dd") +
                 "' where id=" + id + ";";
            int p1 = dbMan.ExecuteNonQuery(query);
            if (p1 == 0)
                return 0;
            query = "Update Management Set salary=" + salary + " where id=" + id + ";";
            return dbMan.ExecuteNonQuery(query) * p1;
        }

        //get user's branch id
        public int Get_user_branch(int id)
        {
            string query = "Select branch_id from users where id=" + id + ";";
            return (int)dbMan.ExecuteScalar(query);
        }

        //get all equipments of manager branch
        public DataTable Get_All_Equipment()
        {
            string query = "Select * from equipment ;";
            return dbMan.ExecuteReader(query);
        }

        //delete equipment with it's code
        public int delete_equipment(object code)
        {
            object c;
            if (code == null) c = "NULL";
            else c = (int)code;
            string query = "Delete From Equipment where code=" + c + ";";
            return dbMan.ExecuteNonQuery(query);
        }

        //insert equipment to the manager's branch
        public int insert_equipment(int code, string name, string status, int branch_id)
        {
            string query = "Insert into Equipment Values (" + code + ",'" + name + "','" + status + "'," + branch_id + ");";
            return dbMan.ExecuteNonQuery(query);
        }

        //to get initial value for equipment PK
        public int Get_new_equipment_PK()
        {
            string query = "Select max(code) from Equipment ;";
            int? p = dbMan.ExecuteScalar(query) as int?;
            if (p == null)
                return 0;
            else
                return (int)p + 1;
        }

        //to get all trainers in the branch
        public DataTable Get_All_Trainers()
        {
            string query = "Select u.id,u.name,t.salary,u.startdate,u.enddate from users u , trainer t where u.id=t.id ;";
            return dbMan.ExecuteReader(query);
        }

        //to get new id for a new trainer
        public int Get_new_Trainer_PK()
        {
            string query = "Select max(id) from Trainer ;";
            int? p = dbMan.ExecuteScalar(query) as int?;
            if (p == null)
                return 2000000;
            else
                return (int)p + 1;
        }

        //to insert a trainer
        public int insert_new_trainer(int id, string name, string gender, DateTime start_date, DateTime end_date, int branch_id, int salary)
        {
            if (start_date > end_date)
                return 0;
            string query = "insert into users(id,password,name,gender,startdate,enddate,branch_id) values(" + id + ",'" + "12345" + "','" + name + "','" + gender + "','" + start_date.ToString("yyyy-MM-dd") + "','" +
                end_date.ToString("yyyy-MM-dd") + "'," + branch_id + ");";
            dbMan.ExecuteNonQuery(query);
            query = "Insert Into trainer values (" + id + "," + salary + ");";
            return dbMan.ExecuteNonQuery(query);
        }

        //delete trainer
        public int delete_trainer(int id)
        {
            string query = "Update trainee Set personal_trainer=NULL Where exists (Select * from trainee where personal_trainer=" + id + ")";
            dbMan.ExecuteNonQuery(query);

            query = "Delete from trainer where id=" + id + ";";
            dbMan.ExecuteNonQuery(query);

            query = "Delete from users where id=" + id + ";";
            return dbMan.ExecuteNonQuery(query);
        }

        //get all trainees from branch
        public DataTable Get_All_Trainees()
        {
            string query = "Select u.id,u.name,u.gender,u.startdate,t.Membership_Length from users u,trainee t where u.id=t.id ;";
            return dbMan.ExecuteReader(query);
        }

        //generate id for new trainee
        public int Get_new_Trainee_PK()
        {
            string query = "Select max(id) from Trainee ;";
            int? p = dbMan.ExecuteScalar(query) as int?;
            if (p == null)
                return 3000000;
            else
                return (int)p + 1;
        }

        //add new trainee
        public int insert_new_trainee(int id, string name, string gender, DateTime start_date, int mem_length, int branch_id)
        {
            string query = "insert into users(id,password,name,gender,startdate,branch_id) values(" + id + ",'" + "12345" + "','" + name + "','" + gender + "','" + start_date.ToString("yyyy-MM-dd") +
                 "'," + branch_id + ");";
            dbMan.ExecuteNonQuery(query);
            query = "Insert Into trainee(id,Membership_Length) values (" + id + "," + mem_length + ");";
            return dbMan.ExecuteNonQuery(query);
        }

        public int delete_trainee(int id)
        {
            string query = "delete from users where id=" + id + ";";
            return dbMan.ExecuteNonQuery(query);
        }


        //get all managers
        public DataTable Get_All_Managers()
        {
            string query = "Select u.id,u.name,t.salary,u.startdate,u.enddate from users u , Management t where u.id=t.id ;";
            return dbMan.ExecuteReader(query);
        }

        //generate new manager PK
        public int Get_new_Manager_PK()
        {
            string query = "Select max(id) from Management ;";
            int? p = dbMan.ExecuteScalar(query) as int?;
            if (p == null)
                return 1000000;
            else
                return (int)p + 1;
        }


        //insert new manager
        public int insert_new_manager(int id, string name, string gender, DateTime start_date, DateTime end_date, int branch_id, int salary)
        {
            if (start_date > end_date)
                return 0;
            string query = "insert into users(id,password,name,gender,startdate,enddate,branch_id) values(" + id + ",'" + "12345" + "','" + name + "','" + gender + "','" + start_date.ToString("yyyy-MM-dd") + "','" +
                end_date.ToString("yyyy-MM-dd") + "'," + branch_id + ");";
            dbMan.ExecuteNonQuery(query);
            query = "Insert Into Management values (" + id + "," + salary + ");";
            return dbMan.ExecuteNonQuery(query);
        }

        //delete manager
        public int delete_manager(int id)
        {
            string query = "Update Gym_branch Set Manager=NULL where exists (Select * from Gym_branch where manager=" + id + ");";
            dbMan.ExecuteNonQuery(query);

            query = "Delete from users where id=" + id + ";";
            return dbMan.ExecuteNonQuery(query);
        }

        //get all branches
        public DataTable Get_All_Branches()
        {
            string query = "Select b.id,b.address,u.name as Manager_name from gym_branch b left outer join Users u on b.manager=u.id";
            return dbMan.ExecuteReader(query);
        }

        //get branch info
        public DataTable Get_Branch_info(int id)
        {
            string query = "Select address,manager from gym_branch where id=" + id + ";";
            return dbMan.ExecuteReader(query);
        }

        public DataTable Get_Branch_info_for_Trainee(int id)
        {
            string query = " Select b.id,b.address,u.name as Manager_name from gym_branch b ,Users u where b.manager=u.id And Branch_id=" + id + ";";
            return dbMan.ExecuteReader(query);
        }



        public int getbranchid(int id)
        {
            string query = "select Branch_id from Users where ID=" + id;
            DataTable dr = dbMan.ExecuteReader(query);
            int branch = Convert.ToInt32(dbMan.ExecuteScalar(query).ToString());
            return branch;
        }



        //generate pk for branch
        public int Get_new_Branch_PK()
        {
            string query = "Select max(id) from Gym_branch ;";
            int? p = dbMan.ExecuteScalar(query) as int?;
            if (p == null)
                return 0;
            else
                return (int)p + 1;
        }

        //insert new branch
        public int insert_new_branch(int id, string address, int? manager)
        {
            object c;
            if (manager == null)
                c = "NULL";
            else c = (int)manager;
            string query = "insert into Gym_branch Values (" + id + ",'" + address + "'," + c + ");";
            return dbMan.ExecuteNonQuery(query);
        }

        //edit branch 
        public int Edit_branch(int id, string address, int? manager)
        {
            object c;
            if (manager == null)
                c = "NULL";
            else c = (int)manager;
            string query = "Update gym_branch Set manager=" + c + ",address='" + address + "' where id=" + id + ";";
            return dbMan.ExecuteNonQuery(query);
        }

        //delete branch
        public int Delete_branch(int id)
        {
            string query = "Delete from Gym_branch where id=" + id + ";";
            return dbMan.ExecuteNonQuery(query);
        }

        public DataTable Get_All_Challenges()
        {
            string query = "Select * from Challenge ;";
            return dbMan.ExecuteReader(query);
        }

        //get challenge by name 
        public DataTable Get_Challenges(string name)
        {
            string query = "Select * from Challenge where name='" + name + "' ;";
            return dbMan.ExecuteReader(query);
        }
        //edit challenge
        public int Edit_challenge(string name, string newname, string info)
        {
            string query = "Update Challenge Set name='" + newname + "',info='" + info + "' Where name='" + name + "' ;";
            return dbMan.ExecuteNonQuery(query);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------


        //------------------------------------------------[Hagar]:Trainer queries ---------------------------------------------------------------------

        public int update_Trainer(int id, object name, object age, object password, string gender, int branch_id, DateTime end_date, int salary)
        {
            object c;
            if ((string)age == "")
                c = "NULL";
            else c = Convert.ToInt32(age);
            string query = "Update Users Set " + "age=" + c + " ,name='" + name + "' ,password='" + password + "',gender='" + gender + "',branch_id=" + branch_id + ",Enddate='" + end_date.ToString("yyyy-MM-dd") +
                 "' where id=" + id + ";";
            int p1 = dbMan.ExecuteNonQuery(query);
            if (p1 == 0)
                return 0;
            query = "Update Trainer Set salary=" + salary + " where id=" + id + ";";
            return dbMan.ExecuteNonQuery(query) * p1;
        }

        public int update_equipment(int id, string status)
        {
            string query = "UPDATE Equipment SET status='" + status + "'WHERE code=" + id + ";";
            int p1 = dbMan.ExecuteNonQuery(query);
            if (p1 == 0)
                return 0;
            return dbMan.ExecuteNonQuery(query) * p1;
        }

        public DataTable Get_MY_Trainees(int id)
        {
            string query = "SELECT *  FROM Trainee T,Users U WHERE T.personal_trainer=" + id + "AND T.ID=U.ID ;";
            DataTable db = dbMan.ExecuteReader(query); ;
            return db;
        }

        public float GetRating(int id)
        {

            string query = "SELECT SUM(Trainer_rate) AS totalrating FROM Trainee WHERE personal_trainer=" + id + " ;";
            int? p = dbMan.ExecuteScalar(query) as int?;
            if (p == null)
                return 0;
            else
                return (float)p;
        }

        public int GetRatingCount(int id)
        {

            string query = "SELECT COUNT(Trainer_rate) AS count FROM Trainee WHERE personal_trainer=" + id + " ;";
            int? p = dbMan.ExecuteScalar(query) as int?;
            if (p == null)
                return 0;
            else
                return (int)p;
        }

        public DataTable Get_class_by_name(string name)
        {
            string query = "SELECT * FROM class WHERE Name='" + name + " ;";
            return dbMan.ExecuteReader(query);
        }

        public DataTable Get_All_Classes()
        {
            string query = "SELECT * FROM class  ;";
            return dbMan.ExecuteReader(query);
        }

        public int insert_new_Class(object name, object day, object start, object end, object description, object id)
        {

            string query = "INSERT INTO class VALUES ('" + name + "','" + description + "','" + start + "','" + end + "','" + day + "'," + id + "  ); ";
            return dbMan.ExecuteNonQuery(query);
        }

        public int Update_Class(string name, string day, string start, string end, string description, object id)
        {
            object c;
            if (description == null) c = "NULL";
            else c = (string)description;
            string query = "UPDATE class SET description ='" + c + "', start_time ='" + start + "',end_time ='" + end + "', Day='" + day + "' ,trainer_id=" + id + "  WHERE Name='" + name + "' ;";
            int p1 = dbMan.ExecuteNonQuery(query);
            if (p1 == 0)
                return 0;
            return dbMan.ExecuteNonQuery(query) * p1;
        }

        public int Remove_Class(string name)
        {
            string query = "Delete From class where Name='" + name + "'; ";
            int p1 = dbMan.ExecuteNonQuery(query);
            return p1;
        }
        public DataTable GetTrainers()
        {
            string query = "Select u.id AS ID,u.name AS Name from users u , trainer t where u.id=t.id ;";
            return dbMan.ExecuteReader(query);
        }
        public int Get_trainer_salary(int id)
        {
            string query = "Select salary from Trainer where id = " + id + ";";
            object p = dbMan.ExecuteScalar(query);
            if (p == null) return 0;
            else return (int)p;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public DataTable ViewALLClasses(int Bid)
        {
            string query = "SELECT C.Name,description,start_time,end_time,Day,U.Name as TraineeName FROM class C, Trainer T,Users U ,Gym_classes G Where trainer_id=T.ID AND T.ID=U.ID AND G.class_Name=C.Name AND G.Branch_id=" + Bid;
            return dbMan.ExecuteReader(query);
        }

        public DataTable ViewMyClasses(int ID)
        {
            string query = "SELECT C.Name,description,start_time,end_time,Day,U.Name as TraineeName FROM class C,Class_Trainee,Users U Where C.Name=class_Name And C.trainer_id=U.ID AND Trainee_id=" + ID;
            return dbMan.ExecuteReader(query);
        }

        public int EnrollinClass(int Id, string classname)
        {
            string query = "INSERT INTO Class_Trainee Values(" + Id + ",'" + classname + "')";
            return dbMan.ExecuteNonQuery(query);
        }

        public int LeaveClass(int Id, string classname)
        {
            string query = "DELETE FROM Class_Trainee WHERE Trainee_id= " + Id + " AND class_Name='" + classname + "'";
            return dbMan.ExecuteNonQuery(query);
        }

        //--------------------- Schedule--------------------------//

        public DataTable Get_My_Track(int myid)
        {
            string query = "SELECT Track FROM Trainee where ID=" + myid;
            return dbMan.ExecuteReader(query);
        }
        public DataTable Get_My_Level(int myid)
        {
            string query = "SELECT FitnessLevel FROM Trainee where ID=" + myid;
            return dbMan.ExecuteReader(query);
        }


        public DataTable Days(int myid)
        {
            string query = "SELECT day FROM Training_schedule,Track,Trainee Where track_level=level AND track_type=Type And level=FitnessLevel AND Type=Track ANd ID=" + myid;
            return dbMan.ExecuteReader(query);
        }

        public DataTable Description(int myid)
        {
            string query = "SELECT T.Description FROM Training_schedule T,Track,Trainee Where track_level=level AND track_type=Type And level=FitnessLevel AND Type=Track ANd ID=" + myid;
            return dbMan.ExecuteReader(query);
        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------

        public DataTable Get_My_Trainer(int myid)
        {

            string query = "Select u.age,u.name,u.branch,u.gender from users u ,Trainer t where t.id=" + myid + "and u.id=t.personal_trainer;";
            return dbMan.ExecuteReader(query);

        }
        public int Get_my_TrainerID(int myid)
        {
            string query = "Select personal_trainer From Trainee Where ID=" + myid + ";";
            int? p = dbMan.ExecuteScalar(query) as int?;
            if (p == null)
                return 0;
            else
                return (int)p;
        }

        public int RateMyTrainer(int id, int rate)
        {

            string query = "Update Trainee Set Trainer_rate=" + rate + " where id=" + id + ";";
            return dbMan.ExecuteNonQuery(query);
        }

        public int update_trainee_info(int id, object name, object age, object password, string gender, int branch_id, int memlen, float sw, float mw, float fw, float cw, int act, int h, string track, int fl)
        {
            object c;
            object sww;
            object mww;
            object fww;
            object cww;
            object actt;
            object hh;
            object memlenn;


            if ((string)age == "")
                c = "NULL";
            else c = Convert.ToInt32(age);
            if (memlen == 0)
                memlenn = "NULL";
            else
                memlenn = memlen;

            if (sw == 0)
                sww = "NULL";
            else
                sww = sw;
            if (fw == 0)
                fww = "NULL";
            else
                fww = fw;
            if (mw == 0)
                mww = "NULL";
            else
                mww = mw;
            if (cw == 0)
                cww = "NULL";
            else
                cww = cw;
            if (act == 0)
                actt = "NULL";
            else
                actt = act;

            if (h == 0)
                hh = "NULL";
            else
                hh = h;



            string query = "Update Users Set " + "age=" + c + " ,name='" + name + "' ,password='" + password + "',gender='" + gender + "',branch_id=" + branch_id + " where id=" + id + ";";
            int p1 = dbMan.ExecuteNonQuery(query);
            query = "Update Trainee Set  start_weight=" + sww + ",Muscle_weight=" + mww + ",Fat_weight=" + fww + ",current_weight=" + cww + ",Active=" + actt + ",Height=" + hh + ",Track='" + track + "',Membership_Length=" + memlenn + ",FitnessLevel=" + fl + " where id=" + id + ";";
            return dbMan.ExecuteNonQuery(query) * p1;


        }
        public DataTable Get_Challenges_info()
        {
            string query = "Select * from challenge;";
            return dbMan.ExecuteReader(query);
        }

        //hakteb el queries hna 3la 7asab el challenges ely hager 3mltaha
        public DataTable Get_Challenge1_trainee_info()
        {
            string query = "Select Name,active from Trainee T , Users U where T.ID=U.ID  ;";
            return dbMan.ExecuteReader(query);
        }
        public DataTable Get_Challenge2_trainee_info()
        {
            string query = "Select Name,Muscle_weight from Trainee T , Users U where T.ID=U.ID  ;";
            return dbMan.ExecuteReader(query);
        }
        public DataTable Get_Challenge3_trainee_info()
        {
            string query = "Select Name,(start_weight-current_weight)AS 'Lost weight' from Trainee T , Users U where T.ID=U.ID  ;";
            return dbMan.ExecuteReader(query);
        }
        public DataTable Get_Tracks()
        {
            string query = "SELECT DISTINCT  Type from Track ;";
            return dbMan.ExecuteReader(query);
        }
        public DataTable Get_Trainee(int id)
        {
            string query = "Select * from Trainee  Where id = " + id + ";";
            return dbMan.ExecuteReader(query);

        }

        public void TerminateConnection()
        {
            dbMan.CloseConnection();
        }

    }
}

