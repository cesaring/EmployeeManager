using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) {

        }
        public DbSet<Employee>? Employees {get;set;}
        public DbSet<Country>? Countries {get;set;}
    }
}