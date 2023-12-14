using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LeaveManagementSystem.Models;
using LeaveManagementSystem.Data;
#nullable disable
namespace LeaveManagementSystem.Controllers;

public class HomeController : Controller
{
   
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbcontext _db;

    public HomeController(ApplicationDbcontext db, ILogger<HomeController> logger)
    {
        _logger = logger;
        _db=db;
    }

    
public class AppException : Exception
{
    public AppException() : base ()
   {}
   public AppException(String message) : base (message)
   {}

   public AppException(String message, Exception inner) : base(message,inner) {}
}

 
    public IActionResult Index()
    {
        
        
        return View();
    }
    public IActionResult Login()
    {
      
        return View();
    }
     public IActionResult Logout()
    {
         HttpContext.Session.SetString("Employeeusername","") ;  
         HttpContext.Session.SetString("managerusername","");
         return RedirectToAction("Index");
    }
public IActionResult ViewLeave()
    {
      
        return View();
    }

public IActionResult LeaveStatus()
    {
      
        return View();
    }
public IActionResult LeaveApproval()
    {
      
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginModel login)
    {
        
        var employeedetails=_db.Login.Find(login.EmployeeUsername);
       
        try{
            if(employeedetails is null){
                throw new AppException("username not correct");

            }
            else{
                    if(login.Employeepassword==employeedetails.Employeepassword){
                       HttpContext.Session.SetString("Employeeusername",employeedetails.EmployeeUsername) ;  
                       HttpContext.Session.SetString("managerusername",employeedetails.managerusername);
                       return View("userdisplay");
                
            } 
        
                else{
                    throw new AppException("Password not correct");
                }
        }
        }
        catch(AppException exception){
            Console.WriteLine(exception);
          
        }
        

       // DatabaseOperation databaseOperation = new DatabaseOperation();
        //if(databaseOperation.validateLogin(login.EmployeeUsername,login.Employeepassword)=="Valid"){
          //  return View("userdisplay");
      //  }
        
        return View();

        
    }

    public IActionResult Update()
    {
      
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(LoginModel login)
    {
        if(ModelState.IsValid){
             var employeedetails=_db.Login.Find(login.EmployeeUsername);
             try{
             if(employeedetails is not null){
            
                employeedetails.Employeepassword=login.Employeepassword;
                _db.Update(employeedetails);
                _db.SaveChanges();
             
             
             }
             else{
                throw new AppException("Invalid username");
             }
             }
             catch(AppException exception){
                Console.WriteLine(exception);
             }
        /*if(login.EmployeeUsername is null){
            return View();
        }
        DatabaseOperation databaseOperation = new DatabaseOperation();
        if(databaseOperation.validatepassword(login.Employeepassword)){
        
        databaseOperation.updateLogin(login.EmployeeUsername,login.Employeepassword);
        return View("Success");
        }*/

        }
        return View();
    }
      


    


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
