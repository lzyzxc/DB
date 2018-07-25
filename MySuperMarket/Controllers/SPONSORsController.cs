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
    public class SPONSORsController : Controller
    {
        private MyMarket db = new MyMarket();

        // GET: SPONSORs
        public ActionResult Index()
        {
            return View(db.SPONSOR.ToList());
        }

        // GET: SPONSORs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SPONSOR sPONSOR = db.SPONSOR.Find(id);
            if (sPONSOR == null)
            {
                return HttpNotFound();
            }
            return View(sPONSOR);
        }

        // GET: SPONSORs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SPONSORs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SPONSOR_ID,SPONSOR_NAME,PHONE_NUMBER")] SPONSOR sPONSOR)
        {
            if (ModelState.IsValid)
            {
                db.SPONSOR.Add(sPONSOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sPONSOR);
        }

        // GET: SPONSORs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SPONSOR sPONSOR = db.SPONSOR.Find(id);
            if (sPONSOR == null)
            {
                return HttpNotFound();
            }
            return View(sPONSOR);
        }

        // POST: SPONSORs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SPONSOR_ID,SPONSOR_NAME,PHONE_NUMBER")] SPONSOR sPONSOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sPONSOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sPONSOR);
        }

        // GET: SPONSORs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SPONSOR sPONSOR = db.SPONSOR.Find(id);
            if (sPONSOR == null)
            {
                return HttpNotFound();
            }
            return View(sPONSOR);
        }

        // POST: SPONSORs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SPONSOR sPONSOR = db.SPONSOR.Find(id);
            db.SPONSOR.Remove(sPONSOR);
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
            var list = db.SPONSOR.Select(n => new { SPONSOR_ID = n.SPONSOR_ID, SPONSOR_NAME = n.SPONSOR_NAME, PHONE_NUMBER = n.PHONE_NUMBER });
            return Json(new { code = 0, msg = "", count = 1000, data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}
