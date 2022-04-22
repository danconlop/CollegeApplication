using System;
using CollegeApplication.Services.Abstractions;
using CollegeApplication.Services.Implementations;
using Share.Dto;

namespace CollegeApplication
{
    public class Menu
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        public Menu()
        {
            _studentService = new StudentService();
            _courseService = new CourseService();
        }
        public bool Show()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Student Registration");
            Console.WriteLine("2. Course registration");
            Console.WriteLine("3. Course assignment");
            Console.WriteLine("4. Evaluate student performance");
            Console.WriteLine("5. Consult student performance");
            Console.WriteLine("X. Any other key to exit");
            Console.Write("Your option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    RegisterStudent();
                    break;
                case "2":
                    Console.Clear();
                    RegisterCourse();
                    break;
                default:
                    return false;
                    
            }

            return true;
        }

        private void RegisterStudent()
        {
            var StudentRegistry = new StudentRegistryDto();

            Console.WriteLine("Please enter the required information to register a new student");
            Console.WriteLine("Student code number: ");
            StudentRegistry.CodeNumber = Console.ReadLine();

            Console.WriteLine("First name: ");
            StudentRegistry.FirstName = Console.ReadLine();

            Console.WriteLine("Last name");
            StudentRegistry.LastName = Console.ReadLine();

            try
            {
                StudentRegistry.Validation();

                _studentService.RegisterStudent(StudentRegistry);

                Console.WriteLine("Student registered correctly.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }

        private void RegisterCourse()
        {
            var courseRegistry = new CourseRegistryDto();

            Console.WriteLine("Please enter the required information to register a new course");
            Console.WriteLine("Title: ");
            courseRegistry.Title = Console.ReadLine();

            Console.WriteLine("Credits: "); ;
            var input = Console.ReadLine();

            var isValid = Int32.TryParse(input, out int credits);

            if (!isValid)
            {
                Console.WriteLine("Please enter a valid number.\nPress any key to continue");
                Console.ReadKey();
                return;
            }

            try
            {
                courseRegistry.Credits = credits;
                courseRegistry.Validation();

                _courseService.RegisterCourse(courseRegistry);
                Console.WriteLine("Course registered correctly");
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
    }
}
