using Share.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Services.Abstractions
{
    public interface ICourseService
    {
        void EditCourse(CourseDto editCourse);
        List<CourseDto> GetAll();
        List<CourseDto> GetAvailable(int studentId);
        public void RegisterCourse(CourseRegistryDto courseRegistry);
    }
}
