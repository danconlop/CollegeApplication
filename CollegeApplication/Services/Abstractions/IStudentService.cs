using Share.Dto;
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
        StudentDto GetByCodeNumber(string codeNumber);
        public void RegisterStudent(StudentRegistryDto studentRegistry);
    }
}
