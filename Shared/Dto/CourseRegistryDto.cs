using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Dto
{
    public class CourseRegistryDto
    {
        public string Title { get; set; }
        public int Credits { get; set; }


        public void Validation()
        {
            if (string.IsNullOrEmpty(Title))
                throw new ArgumentNullException("'Title' must not be empty");

            if (Credits <= 0)
                throw new InvalidOperationException("'Credits' must be higher than 0");
        }
    }
}
