using Microsoft.EntityFrameworkCore;
using Model.Configurations;
using Model.Entities;
using System;
using System.Reflection;

namespace Model
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new StudentConfiguration());
            builder.ApplyConfiguration(new CourseConfiguration());

            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Toma las configuraciones en el assembly automaticamente

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CollegeApp;Integrated Security=True");
        }
    }
}
