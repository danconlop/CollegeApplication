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

        public List<CourseDto> GetAll()
        {
            var courses = context.Courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Credits = c.Credits
            }).ToList();

            if (!courses.Any())
                throw new Exception("There are no courses available");

            return courses;
        }

        //public List<CourseDto> GetAllByStudentId(List<EnrollmentDto> enrollments)
        //{
        //    //var courses = enrollments.ForEach(e => context.Courses.Where(c => !c.Equals(e.CourseId));
        //    //var courses = context.Courses.Where(c => !c.Equals(1))
        //}
    }
}
