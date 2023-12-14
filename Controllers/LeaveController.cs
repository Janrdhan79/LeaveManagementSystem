using Microsoft.AspNetCore.Mvc;
using LeaveManagementSystem.Models;
using Newtonsoft.Json;
using System.Text;
#nullable disable
namespace LeaveManagementSystem.Controllers;

public class LeaveController : Controller
{
    DatabaseOperation databaseOperation = new DatabaseOperation();
    

    public IActionResult LeaveRequest()
    {
      
        return View();
    }
    [HttpPost]
     [ValidateAntiForgeryToken]
    public IActionResult LeaveRequest(Leaverequest leave){
         
            if(ModelState.IsValid){
       databaseOperation.insertleaverequest(leave.leavefrom,leave.leaveto,leave.leavetype,leave.numberofdays,leave.descripition,HttpContext.Session.GetString("Employeeusername"),HttpContext.Session.GetString("managerusername"));
        return View("userdisplay");  
        }
        
            
         return View();
    }
    public ActionResult Leavesummary()  
    
        { try{
             if(HttpContext.Session.GetString("Employeeusername").Length==0  )
             {
             return View("Error");
             }
             else{
            IEnumerable<Leavesummary> leavesummary = databaseOperation.GetLeavesummary(HttpContext.Session.GetString("Employeeusername"));  
            
            return View(leavesummary);  
             
             }
        }
        catch(Exception){
            Console.WriteLine("session expired");
        }
         return View("Error");
        }
    public ActionResult EmployeeLeavesummary(string userid)  
        {  
            IEnumerable<EmployeeLeavesummary> employeeleavesummary = databaseOperation.GetUserLeavesummary(userid);  
            return View(employeeleavesummary);  
        }
    public ActionResult Leavestatus()  
        {  
            try{
             if(HttpContext.Session.GetString("Employeeusername").Length==0 )
             {
             return View("Error");
             }
            IEnumerable<Leavestatus> leavestatus = databaseOperation.GetLeavestatus(HttpContext.Session.GetString("Employeeusername"));  
            return View(leavestatus);  }
            catch(Exception){
            Console.WriteLine("session expired");
        }
         return View("Error");
        }
     public ActionResult LeaveApproval()  
        {  try{
             if(HttpContext.Session.GetString("Employeeusername").Length==0 )
             {
             return View("Error");
             }
            IEnumerable<LeaveApproval> leaveApprovals = databaseOperation.LeaveApproval(HttpContext.Session.GetString("Employeeusername"));  
            return View(leaveApprovals);  
        }
            catch(Exception){
            Console.WriteLine("session expired");
        }
         return View("Error");
        }
    public ActionResult Delete(int id)  
        {   databaseOperation.Deleteleave(id);
     
            return RedirectToAction("Leavestatus");  
        }
    public IActionResult update(){
        return View();
    }
     [HttpPost]
     public IActionResult update(LeaveApproval leaveApproval,int id){
        databaseOperation.updateleave(id,leaveApproval.managercomments,leaveApproval.leavedecision);
         return RedirectToAction("LeaveApproval");
    }
     public IActionResult Feedback(){
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Feedback(Feedback feedback){
        feedback.emailid=Request.Form["emailid"];
        feedback.rating=Convert.ToInt32(Request.Form["rating"]);
        feedback.feedback=Request.Form["feedback"];
        Console.WriteLine(feedback.emailid);
        HttpClient httpClient=new HttpClient();
        string apiUrl="http://localhost:5005/api/Feedback";
        var jsondata = JsonConvert.SerializeObject(feedback);
        var data = new StringContent(jsondata,Encoding.UTF8,"application/json");
        var httpresponse=httpClient.PostAsync(apiUrl,data);
        var result = await httpresponse.Result.Content.ReadAsStringAsync();
        Console.WriteLine(result);
        return View();
    }
}