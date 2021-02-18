using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication11.Models
{
    public class TrainerTopic
    {
        public int Id { get; set; }
        [Required]

        public string TrainerId { get; set; }
        public int TopicId { get; set; }

        public ApplicationUser Trainer { get; set; }
        public Topic Topic { get; set; }

    }
}