using Model.Entities;
using Share.Dto;
using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Services.Abstractions
{
    public interface IStudentService
    {
        void AssignCourse(CourseAssignmentDto courseAssignment);
        void AssignGrade(CourseAssignmentDto courseAssignment, Grade grade);
        void Evaluate(StudentEvaluationDto evaluation);
        StudentDto GetByCodeNumber(string codeNumber);
        StudentEvaluationDto GetEvaluationByCodeNumber(string codeNumber);
        List<Enrollment> GetStudentEnrollments(int studentId);
        public void RegisterStudent(StudentRegistryDto studentRegistry);
    }
}
