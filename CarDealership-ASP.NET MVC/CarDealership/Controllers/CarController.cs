using CarDealership.Models;
using CarDealership.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Controllers
{
    public class CarController : Controller
    {
        ICarRepository _repo = new DBCarRepository();///need to change to mock repo

        // GET: Car
        public ActionResult Index()
        {
            var cars = _repo.GetAllCars();
            return View(cars);
        }

        public ActionResult Details(int id)
        {
            var car = _repo.GetCarById(id);
            return View(car);
        }

        public ActionResult Add()
        {

            return View(new Car());

        }

        public ActionResult Details2(string year, string make, string model)
        {
            Car returnedCar = _repo.GetCarByMMY(year, make, model);

            if (returnedCar == null)
            {
                return RedirectToAction("Index");
            }
            return View("Details", returnedCar);
        }

        [HttpPost]
        public ActionResult Add(Car newCar)
        {
            if (ModelState.IsValid)
            {
                _repo.AddCar(newCar);
                return RedirectToAction("Index");

            }
            else
            {
                return View("Add");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(string username, string password)
        {
            var user = _repo.LoginUser(username, password);
            ViewBag.User = user;
            if (user == null)
            {
                return View("Login");
            }
            else
            {
                var cars = _repo.GetAllCars();
                return View("Index", cars);
            }
        }
    }
}