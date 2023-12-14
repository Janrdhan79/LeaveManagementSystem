namespace LeaveManagementSystem.Models;
   public class LeaveApproval{
        public int leaveid{get; set;}
        public string? userid{get; set;}
        public DateTime leavefrom{get; set;}
        public DateTime leaveto{get; set;}
        public string? leavetype{get; set;}
        public int numberofdays{get;set;}
        public string? descripition{get;set;}

        public string? leavedecision{get; set;}

         public string? managercomments{get; set;}


    }
    