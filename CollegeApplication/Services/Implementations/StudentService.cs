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
    }
}
