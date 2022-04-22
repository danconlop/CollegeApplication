using Share.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Services.Abstractions
{
    interface IEnrollmentService
    {
        List<EnrollmentDto> GetStudentEnrollments(StudentDto student);
    }
}
