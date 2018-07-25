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
    public class SUPPORTsController : Controller
    {
        private MyMarket db = new MyMarket();

        // GET: SUPPORTs
        public ActionResult Index()
        {
            //var sUPPORT = db.SUPPORT.Include(s => s.INCOME).Include(s => s.SPONSOR);
            //return View(sUPPORT.ToList());
            return View();
        }

        // GET: SUPPORTs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPORT sUPPORT = db.SUPPORT.Find(id);
            if (sUPPORT == null)
            {
                return HttpNotFound();
            }
            return View(sUPPORT);
        }

        // GET: SUPPORTs/Create
        public ActionResult Create()
        {
            ViewBag.INCOME_ID = new SelectList(db.INCOME, "INCOME_ID", "TYPE");
            ViewBag.SPONSOR_ID = new SelectList(db.SPONSOR, "SPONSOR_ID", "SPONSOR_NAME");
            return View();
        }

        // POST: SUPPORTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SPONSOR_ID,INCOME_ID,SUPPORT_DATE,MONEY")] SUPPORT sUPPORT)
        {
            if (ModelState.IsValid)
            {
                db.SUPPORT.Add(sUPPORT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.INCOME_ID = new SelectList(db.INCOME, "INCOME_ID", "TYPE", sUPPORT.INCOME_ID);
            ViewBag.SPONSOR_ID = new SelectList(db.SPONSOR, "SPONSOR_ID", "SPONSOR_NAME", sUPPORT.SPONSOR_ID);
            return View(sUPPORT);
        }

        // GET: SUPPORTs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPORT sUPPORT = db.SUPPORT.Find(id);
            if (sUPPORT == null)
            {
                return HttpNotFound();
            }
            ViewBag.INCOME_ID = new SelectList(db.INCOME, "INCOME_ID", "TYPE", sUPPORT.INCOME_ID);
            ViewBag.SPONSOR_ID = new SelectList(db.SPONSOR, "SPONSOR_ID", "SPONSOR_NAME", sUPPORT.SPONSOR_ID);
            return View(sUPPORT);
        }

        // POST: SUPPORTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SPONSOR_ID,INCOME_ID,SUPPORT_DATE,MONEY")] SUPPORT sUPPORT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUPPORT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.INCOME_ID = new SelectList(db.INCOME, "INCOME_ID", "TYPE", sUPPORT.INCOME_ID);
            ViewBag.SPONSOR_ID = new SelectList(db.SPONSOR, "SPONSOR_ID", "SPONSOR_NAME", sUPPORT.SPONSOR_ID);
            return View(sUPPORT);
        }

        // GET: SUPPORTs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPORT sUPPORT = db.SUPPORT.Find(id);
            if (sUPPORT == null)
            {
                return HttpNotFound();
            }
            return View(sUPPORT);
        }

        // POST: SUPPORTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SUPPORT sUPPORT = db.SUPPORT.Find(id);
            db.SUPPORT.Remove(sUPPORT);
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
            var list = db.SUPPORT.Include(n => n.INCOME).Include(n => n.SPONSOR).Select(n => new { SPONSOR_ID = n.SPONSOR_ID, SPONSOR_NAME = n.SPONSOR.SPONSOR_NAME, MONEY = n.MONEY, SUPPORT_DATE = n.SUPPORT_DATE });

            //var pLAN = db.PLAN.Include(p => p.PRODUCT_ATTRIBUTE);
            //var list = db.PLAN.Select(n => new { PLAN_ID = n.PLAN_ID, PRODUCT_ID = n.PRODUCT_ID, PHONE_NUMBER = n. });
            return Json(new { code = 0, msg = "", count = 1000, data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}
