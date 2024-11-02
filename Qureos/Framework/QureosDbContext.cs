using Microsoft.EntityFrameworkCore;
using Qureos.Entity;
using System.Collections.Generic;

namespace Qureos.Framework
{
    public class QureosDbContext : DbContext
    {
        

     
        public QureosDbContext(DbContextOptions<QureosDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTasks> ProjectTasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TasksComment> Comments { get; set; }  // New DbSet for comments

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TasksComment>()
                      .HasOne(c => c.task)
                      .WithMany(t => t.Comments)  
                      .HasForeignKey(c => c.TaskId);

            modelBuilder.Entity<TasksComment>()
                .HasOne(c => c.user)
                .WithMany(u => u.Comments)  
                .HasForeignKey(c => c.UserId);
        }
}
}
