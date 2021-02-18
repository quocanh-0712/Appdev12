﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication11.Models;
using WebApplication11.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace WebApplication11.Controllers
{
    public class TrainerTopicsController : Controller
    {
        private ApplicationDbContext _context;

        public TrainerTopicsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.IsInRole("TrainingStaff"))
            {
                var trainertopics = _context.TrainerTopics.Include(t => t.Topic).Include(t => t.Trainer).ToList();
                return View(trainertopics);
            }
            if (User.IsInRole("Trainer"))
            {
                var trainerId = User.Identity.GetUserId();
                var Res = _context.TrainerTopics.Where(e => e.TrainerId == trainerId).Include(t => t.Topic).ToList();
                return View(Res);
            }
            return View("Login");
        }
        [Authorize(Roles = "TrainingStaff")]

        public ActionResult Create()
        {
            //get trainer
            var role = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic
            var topics = _context.Topics.ToList();

            var TrainerTopicVM = new TrainerTopicViewModel()
            {
                Topics = topics,
                Trainers = users,
                TrainerTopic = new TrainerTopic()
            };

            return View(TrainerTopicVM);
        }

        [HttpPost]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Create(TrainerTopic trainerTopic)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            var checkTrainerInTopic = _context.TrainerTopics.Any(c => c.TrainerId == trainerTopic.TrainerId &&
                                                                       c.TopicId == trainerTopic.TopicId);
            //Check if Trainer Name or Topic Name existed or not
            if (checkTrainerInTopic == true)
            {
                return View("~/Views/TrainerTopics/AssignExistTrainerTopic.cshtml");
            }

            var newTrainerTopic = new TrainerTopic
            {
                TrainerId = trainerTopic.TrainerId,
                TopicId = trainerTopic.TopicId
            };

            _context.TrainerTopics.Add(newTrainerTopic);
            _context.SaveChanges();
            return RedirectToAction("Index", "TrainerTopics");
        }

        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Delete(int id)
        {
            var trainertopicInDb = _context.TrainerTopics.SingleOrDefault(p => p.Id == id);

            if (trainertopicInDb == null)
            {
                return HttpNotFound();
            }
            _context.TrainerTopics.Remove(trainertopicInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "TrainerTopics");

        }
    }
}