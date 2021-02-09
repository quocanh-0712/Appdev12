﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication11.Models;
using WebApplication11.ViewModels;

namespace WebApplication11.Controllers
{
	public class CoursesController : Controller
	{
		private ApplicationDbContext _context;
		public CoursesController()
		{
			_context = new ApplicationDbContext();
		}
		// GET: Course
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Index(string searchString)
		{
			var courses = _context.Courses
			.Include(p => p.Category);

			if (!String.IsNullOrEmpty(searchString))
			{
				courses = courses.Where(
					s => s.Name.Contains(searchString) ||
					s.Category.Name.Contains(searchString));

			}

			return View(courses.ToList());
		}

		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create()
		{
			var viewModel = new CourseCategoryViewModel
			{
				Categories = _context.Categories.ToList(),
			};
			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create(Course course)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			//Check if Course Name existed or not
			if (_context.Courses.Any(c => c.Name == course.Name &&
										  c.CategoryId == course.CategoryId))
			{
				return View("~/Views/Courses/CheckExists.cshtml");
			}

			var newCourse = new Course
			{
				Name = course.Name,
				CategoryId = course.CategoryId,
			};

			_context.Courses.Add(newCourse);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]

		public ActionResult Delete(int id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(p => p.Id == id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}

			_context.Courses.Remove(courseInDb);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]

		public ActionResult Edit(int id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(p => p.Id == id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}

			var viewModel = new CourseCategoryViewModel
			{
				Course = courseInDb,
				Categories = _context.Categories.ToList(),
			};

			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "TrainingStaff")]

		public ActionResult Edit(Course course)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var courseInDb = _context.Courses.SingleOrDefault(p => p.Id == course.Id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}

			courseInDb.Name = course.Name;
			courseInDb.CategoryId = course.CategoryId;
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
		[HttpGet]
		public ActionResult Details(int id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(p => p.Id == id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}

			return View(courseInDb);
		}
	}
}