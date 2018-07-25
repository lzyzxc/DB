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
    public class EXPENSEsController : Controller
    {
        private MyMarket db = new MyMarket();

        // GET: EXPENSEs
        public ActionResult Index()
        {
            return View(db.EXPENSE.ToList());
        }

        // GET: EXPENSEs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXPENSE eXPENSE = db.EXPENSE.Find(id);
            if (eXPENSE == null)
            {
                return HttpNotFound();
            }
            return View(eXPENSE);
        }

        // GET: EXPENSEs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EXPENSEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EXPENSE_ID,EXPENSE_DATE,MONEY,TYPE")] EXPENSE eXPENSE)
        {
            if (ModelState.IsValid)
            {
                db.EXPENSE.Add(eXPENSE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eXPENSE);
        }

        // GET: EXPENSEs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXPENSE eXPENSE = db.EXPENSE.Find(id);
            if (eXPENSE == null)
            {
                return HttpNotFound();
            }
            return View(eXPENSE);
        }

        // POST: EXPENSEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EXPENSE_ID,EXPENSE_DATE,MONEY,TYPE")] EXPENSE eXPENSE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eXPENSE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eXPENSE);
        }

        // GET: EXPENSEs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXPENSE eXPENSE = db.EXPENSE.Find(id);
            if (eXPENSE == null)
            {
                return HttpNotFound();
            }
            return View(eXPENSE);
        }

        // POST: EXPENSEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            EXPENSE eXPENSE = db.EXPENSE.Find(id);
            db.EXPENSE.Remove(eXPENSE);
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
            var list = db.EXPENSE.Select(n => new { EXPENSE_ID = n.EXPENSE_ID, EXPENSE_DATE = n.EXPENSE_DATE, MONEY = n.MONEY, TYPE = n.TYPE });
            return Json(new { code = 0, msg = "", count = 1000, data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}
