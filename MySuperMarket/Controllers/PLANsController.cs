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
    public class PLANsController : Controller
    {
        private MyMarket db = new MyMarket();

        // GET: PLANs
        public ActionResult Index()
        {
            var pLAN = db.PLAN.Include(p => p.PRODUCT_ATTRIBUTE);
            return View(pLAN.ToList());
        }

        // GET: PLANs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLAN pLAN = db.PLAN.Find(id);
            if (pLAN == null)
            {
                return HttpNotFound();
            }
            return View(pLAN);
        }

        // GET: PLANs/Create
        public ActionResult Create()
        {
            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCT_ATTRIBUTE, "PRODUCT_ID", "SUPPLIER_ID");
            return View();
        }

        // POST: PLANs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PLAN_ID,PRODUCT_ID,PLAN_NUM")] PLAN pLAN)
        {
            if (ModelState.IsValid)
            {
                db.PLAN.Add(pLAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCT_ATTRIBUTE, "PRODUCT_ID", "SUPPLIER_ID", pLAN.PRODUCT_ID);
            return View(pLAN);
        }

        // GET: PLANs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLAN pLAN = db.PLAN.Find(id);
            if (pLAN == null)
            {
                return HttpNotFound();
            }
            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCT_ATTRIBUTE, "PRODUCT_ID", "SUPPLIER_ID", pLAN.PRODUCT_ID);
            return View(pLAN);
        }

        // POST: PLANs/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PLAN_ID,PRODUCT_ID,PLAN_NUM")] PLAN pLAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pLAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCT_ATTRIBUTE, "PRODUCT_ID", "SUPPLIER_ID", pLAN.PRODUCT_ID);
            return View(pLAN);
        }

        // GET: PLANs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLAN pLAN = db.PLAN.Find(id);
            if (pLAN == null)
            {
                return HttpNotFound();
            }
            return View(pLAN);
        }

        // POST: PLANs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PLAN pLAN = db.PLAN.Find(id);
            db.PLAN.Remove(pLAN);
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
            var list = db.PLAN.Include(n => n.PRODUCT_ATTRIBUTE).Select(n => new { PLAN_ID = n.PLAN_ID, PRODUCT_ID = n.PRODUCT_ID, PRODUCT_NAME = n.PRODUCT_ATTRIBUTE.PRODUCT_NAME, SUPPLIER_ID = n.PRODUCT_ATTRIBUTE.SUPPLIER_ID, PURCHASE_PRICE = n.PRODUCT_ATTRIBUTE.PURCHASE_PRICE, PLAN_NUM = n.PLAN_NUM });

            //var pLAN = db.PLAN.Include(p => p.PRODUCT_ATTRIBUTE);
            //var list = db.PLAN.Select(n => new { PLAN_ID = n.PLAN_ID, PRODUCT_ID = n.PRODUCT_ID, PHONE_NUMBER = n. });
            return Json(new { code = 0, msg = "", count = 1000, data = list }, JsonRequestBehavior.AllowGet);
        }

    }
}
