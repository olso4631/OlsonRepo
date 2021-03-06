﻿using CarDealership.Models;
using CarDealership.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            ViewBag.Message = "Contact us using the form below!";

            return View();
        }

        [HttpPost]
        public ActionResult Sales(ContactForm contactForm)
        {
            ViewBag.Message = "Contact us using the form below!";

            if (ModelState.IsValid)
            {
                // yay, send a sales guy an email
                return RedirectToAction("Thanks");
            }
            else
            {
                return View("Contact");
            }
        }

        public ActionResult Interest()
        {
            ViewBag.Message = "Give us your info and we'll get back to you!";

            return View();
        }

        [HttpPost]
        public ActionResult Sales2(InterestForm interestForm)
        {
            ViewBag.Message = "Give us your info and we'll get back to you!";

            if (ModelState.IsValid)
            {
                // yay, send a sales guy an email
                return RedirectToAction("GetBackToYou");
            }
            else
            {
                return View("Interest");
            }
        }
        public ActionResult GetBackToYou()
        {
            return View();
        }







        public ActionResult Thanks()
        {
            return View();
        }
    }
}