using System;
using System.ComponentModel.DataAnnotations;
namespace LeaveManagementSystem.Models;
   public class Leaverequest{
         [Required]
         [DataType(DataType.Date)]
        public DateTime leavefrom{get; set;}
        [Required]
        [DataType(DataType.Date)]
        public DateTime leaveto{get; set;}
        [Required]

        public string? leavetype{get; set;}
        [RegularExpression(@"^[0-9]{1,}$",ErrorMessage ="enter valid number")]
        [Required]
       
        public int numberofdays{get;set;}
        [Required]
        public string? descripition{get;set;}
        
    }
    