using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sample.Data;
using Sample.Models;

namespace Sample.Data
{
    public class SGSDbContext : DbContext
    {
       public SGSDbContext(DbContextOptions<SGSDbContext> options) : base(options)
       {
        
       }
       public DbSet<Register> Registrations { get; set; }

    }
}













