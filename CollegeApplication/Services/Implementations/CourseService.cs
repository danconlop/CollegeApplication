using CollegeApplication.Services.Abstractions;
using Model;
using Model.Entities;
using Share.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext context = new AppDbContext();

        public void RegisterCourse(CourseRegistryDto courseRegistry)
        {
            var course = new Course(courseRegistry.Title,courseRegistry.Credits);

            try
            {
                context.Courses.Add(course);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
