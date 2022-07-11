using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PentonixAssesment.Models;

namespace PentonixAssesment.Controllers
{
    public class TodayTasksController : Controller
    {
        private DB_tasks db = new DB_tasks();

        // GET: TodayTasks
        public ActionResult Index()
        {
            return View(db.AssignedTask.ToList());
        }

        // GET: TodayTasks/Details/5
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

        // GET: TodayTasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodayTasks/Create
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

        // GET: TodayTasks/Edit/5
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

        // POST: TodayTasks/Edit/5
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

        // GET: TodayTasks/Delete/5
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

        // POST: TodayTasks/Delete/5
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
    }
}
