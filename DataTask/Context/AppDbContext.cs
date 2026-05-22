using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTask.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataTask.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<ModelUser> Users { get; set; }

        public DbSet<ModelTask> Tasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelTask>().ToTable("tblTask");
            modelBuilder.Entity<ModelTask>().Property(t => t.TaskName).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<ModelTask>().Property(t => t.TaskDescription).IsRequired().HasMaxLength(1000);
            modelBuilder.Entity<ModelTask>().Property(t => t.TaskPriority).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<ModelTask>().Property(t => t.Assignee).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<ModelTask>().Property(t => t.DueDate).IsRequired();
            modelBuilder.Entity<ModelTask>().Property(t => t.IsCompleted).IsRequired();
            modelBuilder.Entity<ModelUser>().ToTable("tblUser");
        }
    }
}
