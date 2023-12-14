using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Models;
   public class LoginModel{
       [Key]
       [Required(ErrorMessage ="username can't be null")]
        public string? EmployeeUsername{get; set;}
                
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",ErrorMessage ="password should contain upper,lower,digit and symbol")]
        public string? Employeepassword{get; set;}

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Employeepassword",ErrorMessage ="password doesnot match")]
        
        public string? Employeeconfirmpassword{get; set;}

         public string? managerusername{get; set;}
    }
    
    