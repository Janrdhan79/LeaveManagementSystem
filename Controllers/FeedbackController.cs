using Microsoft.AspNetCore.Mvc;
using LeaveManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Text;
#nullable disable
namespace LeaveManagementSystem.Controllers;
[Log]
public class FeedbackController : Controller{
    public IActionResult Feedback(){
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Feedback(Feedback feedback){
         if(ModelState.IsValid){
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
        if(Convert.ToBoolean(result)){
            return RedirectToAction("Index","Home");

        }
         }
        return View();
    }

}