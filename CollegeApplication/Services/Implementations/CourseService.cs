using CollegeApplication.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
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
                Credits = c.Credits,
                Limit = c.Limit
            }).ToList();

            if (!courses.Any())
                throw new Exception("There are no courses available");

            return courses;
        }

        public List<CourseDto> GetAvailable(int studentId)
        {
            var courses = context.Courses.Where(c => !c.Enrollments.Select(e => e.StudentId).Contains(studentId)).Select(c => new CourseDto
            //var courses = context.Courses.Include(c => c.Enrollments).Where(c => !c.Enrollments.Select(e => e.StudentId).Contains(studentId)).Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Credits = c.Credits,
                Limit = c.Limit
            }).ToList();

            if (!courses.Any())
                throw new Exception("There are no courses available.");

            return courses;
        }

        public void EditCourse(CourseDto editCourse)
        {
            var course = context.Courses.Include(c => c.Enrollments).FirstOrDefault(c => c.Id.Equals(editCourse.Id));
            if (course is null)
                throw new ApplicationException("Course selected does not exists");

            if (course.Enrollments.Count() > editCourse.Limit)
                throw new ApplicationException("New course capacity must be higher than the current occupancy");
            
            course.Title = editCourse.Title;
            course.Credits = editCourse.Credits;
            course.Limit = editCourse.Limit;

            context.SaveChanges();
        }
    }
}
