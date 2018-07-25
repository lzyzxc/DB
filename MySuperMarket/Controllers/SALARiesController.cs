using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MySuperMarket.Models;

namespace MySuperMarket.Controllers
{
    public class SALARiesController : Controller
    {
        private MyMarket db = new MyMarket();

        // GET: SALARies
        public ActionResult Index()
        {
            var sALARY = db.SALARY.Include(s => s.EMPLOYEE).Include(s => s.EXPENSE);
            return View(sALARY.ToList());
        }

        // GET: SALARies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALARY sALARY = db.SALARY.Find(id);
            if (sALARY == null)
            {
                return HttpNotFound();
            }
            return View(sALARY);
        }

        // GET: SALARies/Create
        public ActionResult Create()
        {
            ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEE, "EMPLOYEE_ID", "EMPLOYEE_NAME");
            ViewBag.EXPENSE_ID = new SelectList(db.EXPENSE, "EXPENSE_ID", "TYPE");
            return View();
        }

        // POST: SALARies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EXPENSE_ID,EMPLOYEE_ID,SALARY_DATE")] SALARY sALARY)
        {
            if (ModelState.IsValid)
            {
                db.SALARY.Add(sALARY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEE, "EMPLOYEE_ID", "EMPLOYEE_NAME", sALARY.EMPLOYEE_ID);
            ViewBag.EXPENSE_ID = new SelectList(db.EXPENSE, "EXPENSE_ID", "TYPE", sALARY.EXPENSE_ID);
            return View(sALARY);
        }

        // GET: SALARies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALARY sALARY = db.SALARY.Find(id);
            if (sALARY == null)
            {
                return HttpNotFound();
            }
            ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEE, "EMPLOYEE_ID", "EMPLOYEE_NAME", sALARY.EMPLOYEE_ID);
            ViewBag.EXPENSE_ID = new SelectList(db.EXPENSE, "EXPENSE_ID", "TYPE", sALARY.EXPENSE_ID);
            return View(sALARY);
        }

        // POST: SALARies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EXPENSE_ID,EMPLOYEE_ID,SALARY_DATE")] SALARY sALARY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sALARY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EMPLOYEE_ID = new SelectList(db.EMPLOYEE, "EMPLOYEE_ID", "EMPLOYEE_NAME", sALARY.EMPLOYEE_ID);
            ViewBag.EXPENSE_ID = new SelectList(db.EXPENSE, "EXPENSE_ID", "TYPE", sALARY.EXPENSE_ID);
            return View(sALARY);
        }

        // GET: SALARies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALARY sALARY = db.SALARY.Find(id);
            if (sALARY == null)
            {
                return HttpNotFound();
            }
            return View(sALARY);
        }

        // POST: SALARies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SALARY sALARY = db.SALARY.Find(id);
            db.SALARY.Remove(sALARY);
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

        public JsonResult getJson()
        {
            var list = db.SALARY.Include(s => s.EMPLOYEE).Include(s => s.EXPENSE).Select(n => new { EMPLOYEE_ID = n.EMPLOYEE_ID, EMPLOYEE_NAME = n.EMPLOYEE.EMPLOYEE_NAME, SALARY_DATE = n.SALARY_DATE, SALARY = n.EMPLOYEE.SALARY });
            return Json(new { code = 0, msg = "", count = 1000, data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}
