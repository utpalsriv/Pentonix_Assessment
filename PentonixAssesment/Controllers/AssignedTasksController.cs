using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PentonixAssesment.Models;

namespace PentonixAssesment.Controllers
{
    public class AssignedTasksController : Controller
    {
        private DB_tasks db = new DB_tasks();
        private DB_Entities _db = new DB_Entities();

        // GET: AssignedTasks
        public ActionResult Index()
        {
            return View(db.AssignedTask.ToList());
        }

        // GET: AssignedTasks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedTask assignedTask = db.AssignedTask.Find(id);
            if (assignedTask == null)
            {
                return HttpNotFound();
            }
            return View(assignedTask);
        }

        // GET: AssignedTasks/Create
        public ActionResult Create()
        {
            dynamic dy = new ExpandoObject();
            var Fname = _db.Registration.ToList();
            List<string> countryrlist = new List<string>();
            foreach (var s in Fname)
            {
                countryrlist.Add(s.FirstName);
            }

            ViewBag.NameList = new SelectList(countryrlist, "Name");


            return View();
        }

        public ActionResult GetName()
        {
            
            var Fname = _db.Registration.ToList();
            List<string> countryrlist = new List<string>();
            foreach (var s in Fname)
            {
                countryrlist.Add(s.FirstName);
            }


            return View(countryrlist);
        }
        // POST: AssignedTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketNo,Name,Task,PlannedEffort,Status,Date")] AssignedTask assignedTask)
        {
            if (ModelState.IsValid)
            {
                db.AssignedTask.Add(assignedTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assignedTask);
        }

        // GET: AssignedTasks/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedTask assignedTask = db.AssignedTask.Find(id);
            if (assignedTask == null)
            {
                return HttpNotFound();
            }
            return View(assignedTask);
        }

        // POST: AssignedTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketNo,Name,Task,PlannedEffort,Status,Date")] AssignedTask assignedTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignedTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assignedTask);
        }

        // GET: AssignedTasks/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedTask assignedTask = db.AssignedTask.Find(id);
            if (assignedTask == null)
            {
                return HttpNotFound();
            }
            return View(assignedTask);
        }

        // POST: AssignedTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AssignedTask assignedTask = db.AssignedTask.Find(id);
            db.AssignedTask.Remove(assignedTask);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }
    }
}
