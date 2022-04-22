using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Dto
{
    public class CourseAssignmentDto
    {
        public string StudentCodeNumber { get; set; }
        public int CourseId { get; set; }
        public void ValidateCourseId()
        {
            if (CourseId  <=0)
                throw new Exception("'Course Id' must be higher than 0.");
        }

        public void ValidateStudentCodeNumber()
        {
            if (string.IsNullOrEmpty(StudentCodeNumber))
                throw new ArgumentNullException("'Enrollment' must not be empty");
        }
    }
}
