using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
namespace LeaveManagementSystem.Models;

#nullable disable
    public class DatabaseOperation
{
    static 	List<string> currentuser = new List<string>();
   static Dictionary<string,string> userdetails= new Dictionary<string,string>();
   static string connectionString = "Server=ASPIRE1512\\SQLEXPRESS;Database=Leave Management System;Trusted_Connection=True;MultipleActiveResultSets=true";
   
   /* static DatabaseOperation()
    {
         
       using (SqlConnection connection = new SqlConnection(connectionString))
        {
             string queryString ="select userid,password from Login";
            SqlCommand command = new SqlCommand(queryString, connection);
            

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {   
                    if(!userdetails.ContainsKey(Convert.ToString(reader[0]))){
                        userdetails.Add(Convert.ToString(reader[0]),Convert.ToString(reader[1]));
                }
                
                
            }
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
            }
             connection.Close();
        }
    }
       */

        

    
         public string validateLogin(string EmployeeUsername,string Employeepassword){
            if(userdetails.ContainsKey(EmployeeUsername)){
                currentuser.Add(EmployeeUsername);
                if(userdetails[EmployeeUsername]==Employeepassword){
                    return "Valid";
                    
                }
                else{
                    return "Invalid username";
                }
            }
            return "not valid";


        }

        public void updateLogin(string EmployeeUsername,string Employeepassword){
            if(userdetails.ContainsKey(EmployeeUsername)){
                userdetails[EmployeeUsername]=Employeepassword;
                string queryString ="UPDATE  Login SET  password= @0 where userid=@1; ";

        try{
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(queryString, connection);
        command.Parameters.AddWithValue("@0",Employeepassword);
        command.Parameters.AddWithValue("@1",EmployeeUsername);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        connection.Close();
        }
        catch(SqlException exception){
            Console.WriteLine(exception);
            
        }
                   
                }
        
            }
            
        public bool validatepassword(string Employeepassword){
                Regex passwordformat= new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
                if(passwordformat.IsMatch(Employeepassword)){
                    return true;
                }
            

            return false;
        }

        
        public void insertleaverequest(DateTime leavefrom,DateTime leaveto,string leavetype,int numberofdays,string description,string userid,string managerid){
             string queryString ="INSERT INTO leaverequest(leavefrom,leaveto,leavetype,numberofdays,description,userid,managerid,managercomments,leavestatus) VALUES (@0,@1,@2,@3,@4,@5,@6,@7,@8);";
            try{
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@0",(DateTime)leavefrom);
                    command.Parameters.AddWithValue("@1",(DateTime)leaveto);
                    command.Parameters.AddWithValue("@2",(string)leavetype);
                    command.Parameters.AddWithValue("@3",(int)numberofdays);
                    command.Parameters.AddWithValue("@4",(string)description);
                    command.Parameters.AddWithValue("@5",(string)userid);
                    command.Parameters.AddWithValue("@6",(string)managerid);
                    command.Parameters.AddWithValue("@7","na");
                    command.Parameters.AddWithValue("@8","pending");
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                }
            catch(SqlException exception)
                {
                 Console.WriteLine(exception);
            
                }


        }
       public IEnumerable<Leavesummary> GetLeavesummary(string userid)  
        {  
            List<Leavesummary> leavesummary = new List<Leavesummary>();  
            using (SqlConnection con = new SqlConnection(connectionString))  
            {  
                string queryString ="select * from leavesummary where userid=@0";
              try{
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(queryString, connection);
        command.Parameters.AddWithValue("@0",userid);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
         while (reader.Read())  
                {  
                    Leavesummary leavestatus1 = new Leavesummary();  
                    leavestatus1.leavetype = (string)reader[1];
                    leavestatus1.leaveeligible= (Int32)reader[2];
                    leavestatus1.leavetaken= (Int32)reader[3];
                    leavestatus1.leaveavailable= (Int32)reader[4];
  
                    leavesummary.Add(leavestatus1);  
                }
        connection.Close();
        }
        catch(SqlException exception){
            Console.WriteLine(exception);
            
        }
                
  
            }  
            return leavesummary;  
        }  
public IEnumerable<EmployeeLeavesummary> GetUserLeavesummary(string userid)  
        {  
            List<EmployeeLeavesummary> leavesummary = new List<EmployeeLeavesummary>();  
            using (SqlConnection con = new SqlConnection(connectionString))  
            {  
                string queryString ="select * from leavesummary where userid=@0";
                 
              try{
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(queryString, connection);
        command.Parameters.AddWithValue("@0",(string)userid);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
         while (reader.Read())  
                {  
                    EmployeeLeavesummary leavestatus1 = new EmployeeLeavesummary();  
                    leavestatus1.leavetype = (string)reader[1];
                    leavestatus1.leaveeligible= (Int32)reader[2];
                    leavestatus1.leavetaken= (Int32)reader[3];
                    leavestatus1.leaveavailable= (Int32)reader[4];
  
                    leavesummary.Add(leavestatus1);  
                }
        connection.Close();
        }
        catch(SqlException exception){
            Console.WriteLine(exception);
            
        }
                
  
            }  
            return leavesummary;  
        }  


    
    public IEnumerable<Leavestatus> GetLeavestatus(string userid)  {
        List<Leavestatus> leavestatus = new List<Leavestatus>();  
 
                string queryString ="select * from leaverequest where userid=@0 order by leaveid desc";
              try{
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(queryString, connection);
        command.Parameters.AddWithValue("@0",userid);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
         while (reader.Read())  
                {  
                    Leavestatus leavestatuses = new Leavestatus();  
                    leavestatuses.leavefrom = (DateTime)reader[0];
                    leavestatuses.leaveto= (DateTime)reader[1];
                    leavestatuses.leavetype= (string)reader[2];
                    leavestatuses.numberofdays= (Int32)reader[3];
                    leavestatuses.descripition= (string)reader[4];
                    leavestatuses.approver= (string)reader[6];
                    leavestatuses.leavedecision= (string)reader[7];
                    leavestatuses. managercomments= (string)reader[8];
                    leavestatuses. leaveid= (int)reader[9];
                    leavestatus.Add(leavestatuses);  
                }
        connection.Close();
        }
        catch(SqlException exception){
            Console.WriteLine(exception);
            
        }
                
  
             
            return leavestatus;  
        

    }
     public IEnumerable<LeaveApproval> LeaveApproval(string userid)  {
        List<LeaveApproval> leaveApprovals = new List<LeaveApproval>();  
 
                string queryString ="select * from leaverequest where managerid=@0";
              try{
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(queryString, connection);
        command.Parameters.AddWithValue("@0",userid);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
         while (reader.Read())  
                {  
                    LeaveApproval leaveapproval = new LeaveApproval();  
                    leaveapproval.leavefrom = (DateTime)reader[0];
                   leaveapproval.leaveto= (DateTime)reader[1];
                   leaveapproval.leavetype= (string)reader[2];
                   leaveapproval.numberofdays= (Int32)reader[3];
                   leaveapproval.descripition= (string)reader[4];
                   leaveapproval.userid= (string)reader[5];
                   leaveapproval.leavedecision=(string)reader[8];
                   leaveapproval. leaveid= (int)reader[9];
                    leaveApprovals.Add(leaveapproval);  
                }
        connection.Close();
        }
        catch(SqlException exception){
            Console.WriteLine(exception);
            
        }
                
  
             
            return leaveApprovals;  
        

    }
    public void Deleteleave(int id){
        string queryString ="delete from leaverequest where leaveid=@0";
        try{
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(queryString, connection);
        command.Parameters.AddWithValue("@0",id);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        connection.Close();
        }
        catch(SqlException exception){
            Console.WriteLine(exception);
        }

    }
     public void updateleave(int id,string managercomments,string leavedecision){
        string queryString ="UPDATE  leaverequest SET  managercomments= @0 , leavestatus=@1 where leaveid=@2;";
        try{
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(queryString, connection);
        command.Parameters.AddWithValue("@0",(string)managercomments);
        command.Parameters.AddWithValue("@1",(string)leavedecision);
        command.Parameters.AddWithValue("@2",(int)id);
        connection.Open();
        command.ExecuteReader();
        if(leavedecision=="Approved"){
            updateleavesummary(id);
            

        }
        connection.Close();
        }
        catch(SqlException exception){
            Console.WriteLine(exception);
        }

    }
    public void updateleavesummary(int id){
            string queryString ="select userid,leavetype,numberofdays from leaverequest where leaveid=@0";
              try{
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(queryString, connection);
        command.Parameters.AddWithValue("@0",id);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
         while (reader.Read())  
                {  
                    string userid=(string)reader[0];
                    string leavetype=(string)reader[1];
                    int noofdays=(Int32)reader[2];
                    updateleavesummary(userid,leavetype,noofdays);

                }
        connection.Close();
        }
        catch(SqlException exception){
            Console.WriteLine(exception);
            
        }
         

    }
    public void updateleavesummary(string userid,string leavetype,int noofdays){
         string queryString ="UPDATE  leavesummary SET  leavestaken=leavestaken+@0 , leavesavailable=leavesavailable-@0 where userid=@1 and leavetype=@2;";
        try{
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(queryString, connection);
        command.Parameters.AddWithValue("@1",(string)userid);
        command.Parameters.AddWithValue("@2",(string)leavetype);
        command.Parameters.AddWithValue("@0",(int)noofdays);
        connection.Open();
        command.ExecuteReader();
        connection.Close();
        }
        catch(SqlException exception){
            Console.WriteLine(exception);
        }

    }
    
       
    }
