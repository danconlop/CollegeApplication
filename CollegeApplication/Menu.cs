using System;
using System.Linq;
using CollegeApplication.Services.Abstractions;
using CollegeApplication.Services.Implementations;
using Share.Dto;

namespace CollegeApplication
{
    public class Menu
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly IEnrollmentService _enrollmentService;
        public Menu()
        {
            _studentService = new StudentService();
            _courseService = new CourseService();
            _enrollmentService = new EnrollmentService();
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
                case "3":
                    Console.Clear();
                    AssignCourse();
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

        private void AssignCourse()
        {
            var courseAssignment = new CourseAssignmentDto();

            Console.WriteLine("Please enter the required information to assign a course");
            Console.Write("Student Code Number: ");

            try
            {
                courseAssignment.StudentCodeNumber = Console.ReadLine();
                courseAssignment.ValidateStudentCodeNumber();

                var student = _studentService.GetByCodeNumber(courseAssignment.StudentCodeNumber);
                var studentEnrollments = _enrollmentService.GetStudentEnrollments(student);
                var courses = _courseService.GetAll();

                Console.WriteLine($"\nStudent Information\nStudent Code Number: {student.CodeNumber}\tName: {student.FirstName} {student.LastName}");
                Console.WriteLine("\nAvailable courses");

                foreach(var course in courses)
                {
                    if (studentEnrollments.FirstOrDefault(c => c.CourseId.Equals(course.Id)) == null)
                        Console.WriteLine($"Id: {course.Id}\tTitle: {course.Title}\tCredits: {course.Credits}");
                }

                Console.WriteLine("\nPlease choose an option");
                Console.Write("Course Id: ");
                var input = Console.ReadLine();

                if (Int32.TryParse(input, out int courseId))
                    courseAssignment.CourseId = courseId;
                else
                    throw new Exception("'Course Id' must be a number.");

                courseAssignment.ValidateCourseId();

                if (studentEnrollments.FirstOrDefault(c => c.CourseId.Equals(courseId)) == null)
                {
                    _studentService.AssignCourse(courseAssignment);
                    Console.WriteLine("This student has been assigned to the course successfully");
                } else
                {
                    Console.WriteLine("This student already has this course assigned");
                }

            } catch (Exception ex)
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
