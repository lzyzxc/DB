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
    public class INCOMEsController : Controller
    {
        private MyMarket db = new MyMarket();

        // GET: INCOMEs
        public ActionResult Index()
        {
            return View(db.INCOME.ToList());
        }

        // GET: INCOMEs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INCOME iNCOME = db.INCOME.Find(id);
            if (iNCOME == null)
            {
                return HttpNotFound();
            }
            return View(iNCOME);
        }

        // GET: INCOMEs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: INCOMEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "INCOME_ID,MONEY,INCOME_DATE,TYPE")] INCOME iNCOME)
        {
            if (ModelState.IsValid)
            {
                db.INCOME.Add(iNCOME);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iNCOME);
        }

        // GET: INCOMEs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INCOME iNCOME = db.INCOME.Find(id);
            if (iNCOME == null)
            {
                return HttpNotFound();
            }
            return View(iNCOME);
        }

        // POST: INCOMEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "INCOME_ID,MONEY,INCOME_DATE,TYPE")] INCOME iNCOME)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNCOME).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iNCOME);
        }

        // GET: INCOMEs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INCOME iNCOME = db.INCOME.Find(id);
            if (iNCOME == null)
            {
                return HttpNotFound();
            }
            return View(iNCOME);
        }

        // POST: INCOMEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            INCOME iNCOME = db.INCOME.Find(id);
            db.INCOME.Remove(iNCOME);
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
            var list = db.INCOME.Select(n => new { INCOME_ID = n.INCOME_ID, INCOME_DATE = n.INCOME_DATE, MONEY = n.MONEY, TYPE = n.TYPE });
            return Json(new { code = 0, msg = "", count = 1000, data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}
