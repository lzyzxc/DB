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
    public class SUPPLIERsController : Controller
    {
        private MyMarket db = new MyMarket();

        // GET: SUPPLIERs
        public ActionResult Index()
        {
            return View(db.SUPPLIER.ToList());
        }

        // GET: SUPPLIERs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPLIER sUPPLIER = db.SUPPLIER.Find(id);
            if (sUPPLIER == null)
            {
                return HttpNotFound();
            }
            return View(sUPPLIER);
        }

        // GET: SUPPLIERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SUPPLIERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SUPPLIER_ID,SUPPLIER_NAME,PHONE_NUMBER")] SUPPLIER sUPPLIER)
        {
            if (ModelState.IsValid)
            {
                db.SUPPLIER.Add(sUPPLIER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sUPPLIER);
        }

        // GET: SUPPLIERs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPLIER sUPPLIER = db.SUPPLIER.Find(id);
            if (sUPPLIER == null)
            {
                return HttpNotFound();
            }
            return View(sUPPLIER);
        }

        // POST: SUPPLIERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SUPPLIER_ID,SUPPLIER_NAME,PHONE_NUMBER")] SUPPLIER sUPPLIER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUPPLIER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sUPPLIER);
        }

        // GET: SUPPLIERs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPLIER sUPPLIER = db.SUPPLIER.Find(id);
            if (sUPPLIER == null)
            {
                return HttpNotFound();
            }
            return View(sUPPLIER);
        }

        // POST: SUPPLIERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SUPPLIER sUPPLIER = db.SUPPLIER.Find(id);
            db.SUPPLIER.Remove(sUPPLIER);
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
            var list = db.SUPPLIER.Select(n => new { SUPPLIER_ID = n.SUPPLIER_ID, SUPPLIER_NAME = n.SUPPLIER_NAME, PHONE_NUMBER = n.PHONE_NUMBER });
            return Json(new { code = 0, msg = "", count = 1000, data = list }, JsonRequestBehavior.AllowGet);
        }



    }
}
