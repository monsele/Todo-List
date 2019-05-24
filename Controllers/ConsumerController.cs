using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DavidProject.Controllers
{
    public class ConsumerController : Controller
    {
        // GET: Consumer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }
        // GET: Consumer/Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: Consumer/Create
        public ActionResult Create()
        {
            return View();
        }
        

        // GET: Consumer/Register
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }


        // GET: Consumer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
       
        // Views for Monthly Task
        public ActionResult MonthlyHome()
        {
            return View();
        }
        public ActionResult MonthlyCreate()
        {
            return View();
        }
    }
}