using Share.Enum;
using CollegeApplication.Services.Abstractions;
using System;
using Model;
using Share.Dto;
using Model.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            //var student = context.Students.Select(s => new StudentDto
            //{
            //    Id = s.Id,
            //    FirstName = s.FirstName,
            //    LastName = s.LastName,
            //    CodeNumber = s.CodeNumber
            //}).FirstOrDefault(s => s.CodeNumber.ToLower().Equals(codeNumber.ToLower()));

            if (student is null)
                throw new Exception("Student does not exists");

            return student;
        } 

        public void AssignCourse(CourseAssignmentDto courseAssignment)
        {
            var student = context.Students.FirstOrDefault(s => s.CodeNumber.ToLower().Equals(courseAssignment.StudentCodeNumber.ToLower()));
            var course = context.Courses.Include(c => c.Enrollments).FirstOrDefault(c => c.Id.Equals(courseAssignment.CourseId));

            if (student is null)
                throw new ArgumentNullException("Student does not exist.");
            if (course is null)
                throw new ArgumentNullException("Course does not exist.");
            if (course.Enrollments.Select(e => e.StudentId).Contains(student.Id))
                throw new Exception("This student is already assigned to this course");
            if (course.Enrollments.Count + 1 > course.Limit)
                throw new Exception("Course capacity full, can't assign more students to this course");

            var enrollment = context.Enrollment.Add(new Enrollment(student.Id, course.Id));

            student.Enrollments.Add(enrollment.Entity);
            course.Enrollments.Add(enrollment.Entity);
            context.SaveChanges();
        }

        public List<Enrollment> GetStudentEnrollments(int studentId)
        {
            var enrollments = context.Enrollment.Where(e => e.StudentId.Equals(studentId)).Select(e => new Enrollment
            {
                CourseId = e.CourseId,
                Grade = e.Grade,
                Course = e.Course
                
            }).ToList();

            if (!enrollments.Any())
                throw new ArgumentOutOfRangeException("This student is not enrolled in any course");

            return enrollments;
        }

        public void AssignGrade(CourseAssignmentDto courseAssignment, Grade grade)
        //public void AssignGrade(CourseAssignmentDto courseAssignment, Grade grade)
        {
            var student = context.Students.FirstOrDefault(s => s.CodeNumber.ToLower().Equals(courseAssignment.StudentCodeNumber.ToLower()));
            var course = context.Courses.Include(c => c.Enrollments).FirstOrDefault(c => c.Id.Equals(courseAssignment.CourseId));

            if (student is null)
                throw new ArgumentNullException("Student does not exist.");
            if (course is null)
                throw new ArgumentNullException("Course does not exist.");

            var enrollment = context.Enrollment
                .FirstOrDefault(e => e.StudentId.Equals(student.Id) && e.CourseId.Equals(course.Id));

            if (enrollment is null)
                throw new Exception("This student is not assigned to this course");

            enrollment.Grade = grade;

            context.SaveChanges();
        }

        public StudentEvaluationDto GetEvaluationByCodeNumber(string codeNumber)
        {
            var enrollments = context.Enrollment.Where(e => e.Student.CodeNumber.ToLower().Equals(codeNumber.ToLower())).Select(e => new StudentEnrollmentDto
            {
                CourseId = e.Course.Id,
                Title = e.Course.Title,
                Grade = e.Grade
            }).ToList();

            var evaluation = context.Students.Where(s => s.CodeNumber.ToLower().Equals(codeNumber.ToLower())).Select(s => new StudentEvaluationDto
            {
                Student = new StudentDto
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    CodeNumber = s.CodeNumber
                },
                Enrollments = enrollments
            }).FirstOrDefault();

            if (evaluation is null)
                throw new ArgumentNullException("Student doesn't exist.");

            if (!enrollments.Any())
                throw new Exception("There are either no courses assigned to this student or all courses have been evaluated");

            return evaluation;
        }

        public void Evaluate(StudentEvaluationDto evaluation)
        {
            var enrollments = context.Enrollment.Where(e => e.StudentId.Equals(evaluation.Student.Id)).ToList();

            try
            {
                foreach(var enrollment in enrollments)
                {
                    var x = evaluation.Enrollments.First(e => e.CourseId.Equals(enrollment.CourseId));
                    enrollment.Grade = x.Grade;
                }

                context.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }
}
