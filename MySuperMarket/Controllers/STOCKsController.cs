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
    public class STOCKsController : Controller
    {
        private MyMarket db = new MyMarket();

        // GET: STOCKs
        public ActionResult Index()
        {
            var sTOCK = db.STOCK.Include(s => s.EXPENSE).Include(s => s.PLAN).Include(s => s.PRODUCT);
            return View(sTOCK.ToList());
        }

        // GET: STOCKs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCK sTOCK = db.STOCK.Find(id);
            if (sTOCK == null)
            {
                return HttpNotFound();
            }
            return View(sTOCK);
        }

        // GET: STOCKs/Create
        public ActionResult Create()
        {
            ViewBag.EXPENSE_ID = new SelectList(db.EXPENSE, "EXPENSE_ID", "TYPE");
            ViewBag.PLAN_ID = new SelectList(db.PLAN, "PLAN_ID", "PRODUCT_ID");
            ViewBag.BATCH_ID = new SelectList(db.PRODUCT, "BATCH_ID", "PRODUCT_ID");
            return View();
        }

        // POST: STOCKs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "STOCK_ID,BATCH_ID,PLAN_ID,EXPENSE_ID,STOCK_NUM,STOCK_DATE")] STOCK sTOCK)
        {
            if (ModelState.IsValid)
            {
                db.STOCK.Add(sTOCK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EXPENSE_ID = new SelectList(db.EXPENSE, "EXPENSE_ID", "TYPE", sTOCK.EXPENSE_ID);
            ViewBag.PLAN_ID = new SelectList(db.PLAN, "PLAN_ID", "PRODUCT_ID", sTOCK.PLAN_ID);
            ViewBag.BATCH_ID = new SelectList(db.PRODUCT, "BATCH_ID", "PRODUCT_ID", sTOCK.BATCH_ID);
            return View(sTOCK);
        }

        // GET: STOCKs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCK sTOCK = db.STOCK.Find(id);
            if (sTOCK == null)
            {
                return HttpNotFound();
            }
            ViewBag.EXPENSE_ID = new SelectList(db.EXPENSE, "EXPENSE_ID", "TYPE", sTOCK.EXPENSE_ID);
            ViewBag.PLAN_ID = new SelectList(db.PLAN, "PLAN_ID", "PRODUCT_ID", sTOCK.PLAN_ID);
            ViewBag.BATCH_ID = new SelectList(db.PRODUCT, "BATCH_ID", "PRODUCT_ID", sTOCK.BATCH_ID);
            return View(sTOCK);
        }

        // POST: STOCKs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "STOCK_ID,BATCH_ID,PLAN_ID,EXPENSE_ID,STOCK_NUM,STOCK_DATE")] STOCK sTOCK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTOCK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EXPENSE_ID = new SelectList(db.EXPENSE, "EXPENSE_ID", "TYPE", sTOCK.EXPENSE_ID);
            ViewBag.PLAN_ID = new SelectList(db.PLAN, "PLAN_ID", "PRODUCT_ID", sTOCK.PLAN_ID);
            ViewBag.BATCH_ID = new SelectList(db.PRODUCT, "BATCH_ID", "PRODUCT_ID", sTOCK.BATCH_ID);
            return View(sTOCK);
        }

        // GET: STOCKs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCK sTOCK = db.STOCK.Find(id);
            if (sTOCK == null)
            {
                return HttpNotFound();
            }
            return View(sTOCK);
        }

        // POST: STOCKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            STOCK sTOCK = db.STOCK.Find(id);
            db.STOCK.Remove(sTOCK);
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
            var list = db.STOCK.Include(n => n.EXPENSE).Include(n => n.PLAN).Include(n => n.PRODUCT).Select(n => new { STOCK_ID = n.STOCK_ID, BATCH_ID = n.PRODUCT.BATCH_ID, PRODUCT_ID = n.PRODUCT.PRODUCT_ID, PRODUCT_NAME = n.PRODUCT.PRODUCT_ATTRIBUTE.PRODUCT_NAME, PLAN_ID = n.PLAN_ID, STOCK_NUM = n.STOCK_NUM, STOCK_DATE = n.STOCK_DATE });

            //var pLAN = db.PLAN.Include(p => p.PRODUCT_ATTRIBUTE);
            //var list = db.PLAN.Select(n => new { PLAN_ID = n.PLAN_ID, PRODUCT_ID = n.PRODUCT_ID, PHONE_NUMBER = n. });
            return Json(new { code = 0, msg = "", count = 1000, data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}
