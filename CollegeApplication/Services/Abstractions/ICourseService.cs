﻿using Share.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApplication.Services.Abstractions
{
    public interface ICourseService
    {
        List<CourseDto> GetAll();
        public void RegisterCourse(CourseRegistryDto courseRegistry);
    }
}
