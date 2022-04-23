using Share.Enum;
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
            Console.WriteLine("6. Edit course"); // 
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
                case "4":
                    Console.Clear();
                    //AssignGrade();
                    EvaluateStudentPerformance();
                    break;
                case "5":
                    Console.Clear();
                    ConsultStudentPerformance();
                    Console.ReadKey();
                    break;
                case "6":
                    Console.Clear();
                    EditCourse();
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
                var courses = _courseService.GetAvailable(student.Id);

                Console.WriteLine($"\nStudent Information\nStudent Code Number: {student.CodeNumber}\tName: {student.FirstName} {student.LastName}");
                Console.WriteLine("\nAvailable courses");

                foreach(var course in courses)
                {
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

                _studentService.AssignCourse(courseAssignment);
                Console.WriteLine("This student has been assigned to the course successfully");

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

        //private void AssignGrade()
        //{
        //    var courseInfo = new CourseAssignmentDto();
        //    var gradeAssignmentsDto = new StudentEvaluationDto();

        //    Console.WriteLine("Please enter the required information to assign a grade");
        //    Console.Write("Student Code Number: ");

        //    try
        //    {
        //        courseInfo.StudentCodeNumber = Console.ReadLine();
        //        courseInfo.ValidateStudentCodeNumber();

        //        var student = _studentService.GetByCodeNumber(courseInfo.StudentCodeNumber);
        //        var enrollments = _studentService.GetStudentEnrollments(student.Id);
        //        string cont = "N";

        //        Console.WriteLine($"\nStudent Information\nStudent Code Number: {student.CodeNumber}\tName: {student.FirstName} {student.LastName}");

        //        do
        //        {
        //            Console.WriteLine("\nEnrolled courses");

        //            foreach (var course in enrollments)
        //            {
        //                Console.WriteLine($"{course.CourseId}: {course.Course.Title} \t\tGrade: {course.Grade}");
        //            }

        //            Console.WriteLine("** Grades option are the following: A - B - C - D - F");
        //            Console.WriteLine("\nPlease choorse a course to evaluate");
        //            Console.Write("Course Id: ");
        //            var input = Console.ReadLine();
                    

        //            if (Int32.TryParse(input, out int courseId))
        //                courseInfo.CourseId = courseId;
        //            else
        //                throw new Exception("'Course Id' must be a number");

        //            courseInfo.ValidateCourseId();

        //            Console.Write("Assign a grade: ");
        //            var inputGrade = Console.ReadLine();
        //            Grade newGrade;
        //            var didParse = Enum.TryParse(inputGrade, out newGrade);

        //            if(didParse)
        //            {
        //                //_studentService.AssignGrade(courseInfo, newGrade);
        //                gradeAssignmentsDto.CourseId = courseInfo.CourseId;
        //                gradeAssignmentsDto.StudentId = student.Id;
        //                gradeAssignmentsDto.Grade = newGrade;
        //            }
        //            else
        //            {
        //                Console.WriteLine("Grade only can be (A, B, C, D or F)");
        //            }

        //            Console.WriteLine("Do you want to assign another grade? (y/n)");
        //            cont = Console.ReadLine();
        //        } while (cont.ToLower().Equals("y"));

        //        // Agregar grades
                
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        Console.WriteLine("Press any key to continue");
        //        Console.ReadKey();
        //    }
        //}

        private StudentEvaluationDto ConsultStudentPerformance()
        {
            Console.WriteLine("Please enter the required information to consult the student performance");
            Console.Write("Student Code Number: ");
            var input = Console.ReadLine();

            try
            {
                var evaluation = _studentService.GetEvaluationByCodeNumber(input);

                Console.WriteLine($"\nStudent information\nStudent Code number: {evaluation.Student.CodeNumber}\tName: {evaluation.Student.FirstName} {evaluation.Student.LastName}");
                Console.WriteLine("\nEnrolled courses");

                foreach(var enrollment in evaluation.Enrollments)
                {
                    var grade = enrollment.Grade == null ? "-" : enrollment.Grade.ToString();
                    Console.WriteLine($"{enrollment.CourseId}. {enrollment.Title}\t\t{grade}");
                }

                return evaluation;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        private void EvaluateStudentPerformance()
        {
            var con = false;
            var isValidUpdate = false;
            var evaluation = ConsultStudentPerformance();

            if (evaluation is null)
                return;

            Console.WriteLine("\n** Grade options are the following: A - B - C - D - F");

            do
            {

                Console.WriteLine("\nPlease choose a course to evaluate");
                Console.Write("Course Id: ");
                var inputId = Console.ReadLine();
                

                var isValid = Int32.TryParse(inputId, out int id);
                
                var exists = evaluation.Enrollments.Exists(e => e.CourseId.Equals(id));

                if (!exists)
                {
                    Console.WriteLine("The course you entered is not correct");
                    return;
                }

                if (!isValid)
                {
                    Console.WriteLine("You must enter a valid number");
                    return;
                }

                Console.Write("Assign a grade: ");
                var inputGrade = Console.ReadLine();
                isValid = Enum.TryParse(inputGrade, out Grade grade);

                if (!isValid)
                {
                    Console.WriteLine("You must enter a valid number");
                    return;
                }

                var index = evaluation.Enrollments.FindIndex(e => e.CourseId.Equals(id));
                evaluation.Enrollments.ElementAt(index).Grade = grade;

                Console.WriteLine("Do you want to assign another grade? (y/n)");
                var input = Console.ReadLine();

                con = input.ToLower().Equals("y");

                if (!con)
                    isValidUpdate = true;

            } while (con);

            if (!isValidUpdate)
                return;

            _studentService.Evaluate(evaluation);
        }

        private void EditCourse()
        {
            // Mostrar lista de cursos
            Console.WriteLine("Please enter the required information to edit a course");
            var courses = _courseService.GetAll();

            foreach(var course in courses)
            {
                Console.WriteLine($"Id: {course.Id}\tTitle: {course.Title}\tCredits: {course.Credits}\tCapacity: {course.Limit}");
            }
            try
            {
                Console.WriteLine("\nPlease choose an option");
                Console.Write("Course Id: ");
                var input = Console.ReadLine();
                if (Int32.TryParse(input, out int courseId))
                {
                    Console.Write("Title: ");
                    var title = Console.ReadLine();
                    Console.Write("Credits: ");
                    var inputCredits = Console.ReadLine();
                    if (!Int32.TryParse(inputCredits, out int credits))
                        throw new InvalidCastException("'Credit' must be a number");


                    Console.Write("Capacity: ");
                    var inputCapacity = Console.ReadLine();
                    if (!Int32.TryParse(inputCapacity, out int capacity))
                        throw new InvalidCastException("'Capacity' must be a number");

                    var course = new CourseDto();

                    course.Id = courseId;
                    course.Title = title;
                    course.Credits = credits;
                    course.Limit = capacity;

                    course.Validation();

                    _courseService.EditCourse(course);

                }
                else
                {
                    throw new Exception("'Course Id' must be a number.");
                }
            }
            catch(Exception ex)
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
