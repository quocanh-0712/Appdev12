using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication11.Models;

namespace WebApplication11.ViewModel
{
    public class TopicCourseViewModel
    {
        public Topic Topic { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}