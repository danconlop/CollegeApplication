using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Dto
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int Limit { get; set; }

        public void Validation()
        {
            if (string.IsNullOrEmpty(Title))
                throw new ArgumentNullException("'Title' must not be empty");

            if (Credits <= 0)
                throw new InvalidOperationException("'Credits' must be higher than 0");

            if (Limit <= 0)
                throw new InvalidOperationException("'Limit' must be higher than 0");
        }
    }
}
