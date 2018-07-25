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
    public class SALES_LOTController : Controller
    {
        private MyMarket db = new MyMarket();

        // GET: SALES_LOT
        public ActionResult Index()
        {
            var sALES_LOT = db.SALES_LOT.Include(s => s.INCOME);
            return View(sALES_LOT.ToList());
        }

        // GET: SALES_LOT/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALES_LOT sALES_LOT = db.SALES_LOT.Find(id);
            if (sALES_LOT == null)
            {
                return HttpNotFound();
            }
            return View(sALES_LOT);
        }

        // GET: SALES_LOT/Create
        public ActionResult Create()
        {
            ViewBag.INCOME_ID = new SelectList(db.INCOME, "INCOME_ID", "TYPE");
            return View();
        }

        // POST: SALES_LOT/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BATCH_ID,LOT_DATE,INCOME_ID,MONEY,LOT_NUMBER")] SALES_LOT sALES_LOT)
        {
            if (ModelState.IsValid)
            {
                db.SALES_LOT.Add(sALES_LOT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.INCOME_ID = new SelectList(db.INCOME, "INCOME_ID", "TYPE", sALES_LOT.INCOME_ID);
            return View(sALES_LOT);
        }

        // GET: SALES_LOT/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALES_LOT sALES_LOT = db.SALES_LOT.Find(id);
            if (sALES_LOT == null)
            {
                return HttpNotFound();
            }
            ViewBag.INCOME_ID = new SelectList(db.INCOME, "INCOME_ID", "TYPE", sALES_LOT.INCOME_ID);
            return View(sALES_LOT);
        }

        // POST: SALES_LOT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BATCH_ID,LOT_DATE,INCOME_ID,MONEY,LOT_NUMBER")] SALES_LOT sALES_LOT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sALES_LOT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.INCOME_ID = new SelectList(db.INCOME, "INCOME_ID", "TYPE", sALES_LOT.INCOME_ID);
            return View(sALES_LOT);
        }

        // GET: SALES_LOT/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALES_LOT sALES_LOT = db.SALES_LOT.Find(id);
            if (sALES_LOT == null)
            {
                return HttpNotFound();
            }
            return View(sALES_LOT);
        }

        // POST: SALES_LOT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SALES_LOT sALES_LOT = db.SALES_LOT.Find(id);
            db.SALES_LOT.Remove(sALES_LOT);
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
            var list = db.SALES_LOT.Include(s => s.PRODUCT).Select(n => new { BATCH_ID = n.BATCH_ID, PRODUCT_ID = n.PRODUCT.PRODUCT_ID, PRODUCT_NAME = n.PRODUCT.PRODUCT_ATTRIBUTE.PRODUCT_NAME, LOT_NUMBER = n.LOT_NUMBER, MONEY = n.MONEY, LOT_DATE = n.LOT_DATE });
            return Json(new { code = 0, msg = "", count = 1000, data = list }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getJson1()
        {

            var list = db.SALES_LOT.Include(s => s.PRODUCT).Select(n => new { PRODUCT_ID = n.PRODUCT.PRODUCT_ID, PRODUCT_NAME = n.PRODUCT.PRODUCT_ATTRIBUTE.PRODUCT_NAME });
            return Json(new { code = 0, msg = "", count = 1000, data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}
