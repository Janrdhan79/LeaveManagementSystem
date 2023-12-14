using System;
using System.ComponentModel.DataAnnotations;
namespace LeaveManagementSystem.Models;
   public class Leavesummary{
        public string? leavetype{get; set;}
      
        public int leaveeligible{get;set;}
        public int leavetaken{get;set;}
        public int leaveavailable{get;set;}
       
    }