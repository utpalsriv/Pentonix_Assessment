using PentonixAssesment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PentonixAssesment.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        private DB_Entities _db = new DB_Entities();
        // GET: Registration
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Registration _registration)
        {


            if (ModelState.IsValid)
            {
                var check = _db.Registration.FirstOrDefault(s => s.Email == _registration.Email);
                if (check == null)
                {
                    _registration.Password = GetMD5(_registration.Password);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Registration.Add(_registration);
                    _db.SaveChanges();
                    RedirectToRoute("LoginController", "Login");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();





        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}