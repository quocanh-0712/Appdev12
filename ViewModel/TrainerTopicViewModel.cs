using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication11.Models;

namespace WebApplication11.ViewModels
{
    public class TrainerTopicViewModel
    {
        public Topic Topic { get; set; }
        public TrainerTopic TrainerTopic { get; set; }
        public IEnumerable<ApplicationUser> Trainers { get; set; }
        public IEnumerable<Topic> Topics { get; set; }

    }
}