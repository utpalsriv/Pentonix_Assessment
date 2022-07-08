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
    public class LoginController : Controller
    {
        // GET: Login
        private DB_Entities _db = new DB_Entities();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // GET: Login
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(loginModel.Password);
                var data = _db.Registration.Where(s => s.Email.Equals(loginModel.Email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    if (data[0].Type == "user")
                    {
                        Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                        Session["Email"] = data.FirstOrDefault().Email;
                        ViewBag.Name = data[0].FirstName;
                        return View("SuccessfulLogin");
                    }
                    else {
                        Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                        Session["Email"] = data.FirstOrDefault().Email;
                        ViewBag.Name = data[0].FirstName;
                        return View("AdminSuccessfulLogin");
                    }
                    //add session
                    
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }

            return View("SuccessfulLogin");
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }



        //create a string MD5
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