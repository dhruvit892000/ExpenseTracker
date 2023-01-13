using Exptracker2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Exptracker2.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)// defalt constructor
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<TotalExplim> TotalExplims { get; set; }
    }
}
