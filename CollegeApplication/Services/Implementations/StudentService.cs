
using CollegeApplication.Services.Abstractions;
using System;
using Model;
using Share.Dto;
using Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext context = new AppDbContext();

        public void RegisterStudent(StudentRegistryDto studentRegistry)
        {
            var student = new Student(studentRegistry.CodeNumber, studentRegistry.FirstName, studentRegistry.LastName);

            try
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public StudentDto GetByCodeNumber(string codeNumber)
        {
            var student = context.Students.Select(s => new StudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                CodeNumber = s.CodeNumber
            }).FirstOrDefault(s => s.CodeNumber.ToLower().Equals(codeNumber.ToLower()));

            if (student is null)
                throw new Exception("Student does not exists");

            return student;
        } 

        public void AssignCourse(CourseAssignmentDto courseAssignment)
        {
            var student = context.Students.FirstOrDefault(s => s.CodeNumber.ToLower().Equals(courseAssignment.StudentCodeNumber.ToLower()));
            var course = context.Courses.FirstOrDefault(c => c.Id.Equals(courseAssignment.CourseId));

            if (student is null)
                throw new ArgumentNullException("Student does not exist.");
            if (course is null)
                throw new ArgumentNullException("Course does not exist.");

            var enrollment = context.Enrollment.Add(new Enrollment(student.Id, course.Id));

            student.Enrollments.Add(enrollment.Entity);
            course.Enrollments.Add(enrollment.Entity);
            context.SaveChanges();
        }
    }
}
