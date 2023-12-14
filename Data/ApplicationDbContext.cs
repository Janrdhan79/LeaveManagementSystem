using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Models;
namespace LeaveManagementSystem.Data;
#nullable disable
public class ApplicationDbcontext : DbContext
{
    public  ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
    {
        
    }
    public DbSet<LoginModel> Login{get;set;}
}