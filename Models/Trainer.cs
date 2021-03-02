using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication11.Models
{
    public class Trainer: ApplicationUser
    {
        public string WorkingPlace { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }

    }
}