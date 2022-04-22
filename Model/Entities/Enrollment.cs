using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Enrollment
    {
        public int id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public Grade? Grade { get; set; }
        public virtual Course Course { get; set; } //propiedad de navegacion
        public virtual Student Student { get; set; } //propiedad de navegacion
        public Enrollment()
        {

        }
    }
}
