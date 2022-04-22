using CollegeApplication.Services.Abstractions;
using Model;
using Share.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Services.Implementations
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext context = new AppDbContext();

        public List<EnrollmentDto> GetStudentEnrollments(StudentDto student)
        {
            var enrollment = context.Enrollment.Where(e => e.StudentId.Equals(student.Id)).Select(e => new EnrollmentDto
            {
                id = e.id,
                CourseId = e.CourseId,
                StudentId = e.StudentId
            }).ToList();

            if (!enrollment.Any())
                throw new ArgumentOutOfRangeException("There are no enrollment found with the provided data");

            return enrollment;
        }
    }
}
