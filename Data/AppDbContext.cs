using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
            .HasMany(e => e.expenses)
            .WithOne(d => d.category)
            .OnDelete(DeleteBehavior.NoAction);
        }
        public DbSet<Expenses> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
    }
}