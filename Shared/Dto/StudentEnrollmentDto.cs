using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Dto
{
    public class StudentEnrollmentDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public Grade? Grade { get; set; }
    }
}
