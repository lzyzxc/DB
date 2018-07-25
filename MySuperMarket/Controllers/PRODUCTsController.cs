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
    public class PRODUCTsController : Controller
    {
        private MyMarket db = new MyMarket();

        // GET: PRODUCTs
        public ActionResult Index()
        {
            var pRODUCT = db.PRODUCT.Include(p => p.PRODUCT_ATTRIBUTE);
            return View(pRODUCT.ToList());
        }


        public ActionResult Put()
        {
            var pRODUCT = db.PRODUCT.Include(p => p.PRODUCT_ATTRIBUTE);
            return View(pRODUCT.ToList());
        }



        // GET: PRODUCTs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // GET: PRODUCTs/Create
        public ActionResult Create()
        {
            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCT_ATTRIBUTE, "PRODUCT_ID", "SUPPLIER_ID");
            return View();
        }

        // POST: PRODUCTs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BATCH_ID,PRODUCT_ID,PRODUCT_DATE,DISCOUNT,BATCH_NUMBER")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCT.Add(pRODUCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCT_ATTRIBUTE, "PRODUCT_ID", "SUPPLIER_ID", pRODUCT.PRODUCT_ID);
            return View(pRODUCT);
        }

        // GET: PRODUCTs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCT_ATTRIBUTE, "PRODUCT_ID", "SUPPLIER_ID", pRODUCT.PRODUCT_ID);
            return View(pRODUCT);
        }

        // POST: PRODUCTs/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BATCH_ID,PRODUCT_ID,PRODUCT_DATE,DISCOUNT,BATCH_NUMBER")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCT_ATTRIBUTE, "PRODUCT_ID", "SUPPLIER_ID", pRODUCT.PRODUCT_ID);
            return View(pRODUCT);
        }

        // GET: PRODUCTs/Delete/5
        public ActionResult Delete(string id)
        {
            var pRODUCT = db.PRODUCT.Include(p => p.PRODUCT_ATTRIBUTE);
            return View(pRODUCT.ToList());
        }

        // POST: PRODUCTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            db.PRODUCT.Remove(pRODUCT);
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
        public JsonResult getJson1()
        {
            var list = db.PRODUCT.Include(n => n.PRODUCT_ATTRIBUTE).Select(n => new { BATCH_ID = n.BATCH_ID, PRODUCT_ID = n.PRODUCT_ID, PRODUCT_NAME = n.PRODUCT_ATTRIBUTE.PRODUCT_NAME, PRODUCT_DATE = n.PRODUCT_DATE, BATCH_NUMBER = n.BATCH_NUMBER });

            //var pLAN = db.PLAN.Include(p => p.PRODUCT_ATTRIBUTE);
            //var list = db.PLAN.Select(n => new { PLAN_ID = n.PLAN_ID, PRODUCT_ID = n.PRODUCT_ID, PHONE_NUMBER = n. });
            return Json(new { code = 0, msg = "", count = 1000, data = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getJson2()
        {
            var list = db.PRODUCT.Include(n => n.PRODUCT_ATTRIBUTE).Select(n => new { BATCH_ID = n.BATCH_ID, PRODUCT_ID = n.PRODUCT_ID, PRODUCT_NAME = n.PRODUCT_ATTRIBUTE.PRODUCT_NAME, DISCOUNT = n.DISCOUNT });

            //var pLAN = db.PLAN.Include(p => p.PRODUCT_ATTRIBUTE);
            //var list = db.PLAN.Select(n => new { PLAN_ID = n.PLAN_ID, PRODUCT_ID = n.PRODUCT_ID, PHONE_NUMBER = n. });
            return Json(new { code = 0, msg = "", count = 1000, data = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getJson3()
        {
            var list = db.PRODUCT.Include(n => n.PRODUCT_ATTRIBUTE).Select(n => new { BATCH_ID = n.BATCH_ID, PRODUCT_ID = n.PRODUCT_ID, PRODUCT_NAME = n.PRODUCT_ATTRIBUTE.PRODUCT_NAME, PRODUCT_DATE = n.PRODUCT_DATE, EXP = n.PRODUCT_ATTRIBUTE.EXP, DISCOUNT = n.DISCOUNT });

            //var pLAN = db.PLAN.Include(p => p.PRODUCT_ATTRIBUTE);
            //var list = db.PLAN.Select(n => new { PLAN_ID = n.PLAN_ID, PRODUCT_ID = n.PRODUCT_ID, PHONE_NUMBER = n. });
            return Json(new { code = 0, msg = "", count = 1000, data = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getJson4()
        {
            var list = db.PRODUCT.Include(n => n.SHELF).Select(n => new { BATCH_ID = n.BATCH_ID, PRODUCT_ID = n.PRODUCT_ID, PRODUCT_NAME = n.PRODUCT_ATTRIBUTE.PRODUCT_NAME, SHELF_ID = n.SHEIF_ID, SHELF_AREA = n.SHELF.SHELF_AREA });

            //var pLAN = db.PLAN.Include(p => p.PRODUCT_ATTRIBUTE);
            //var list = db.PLAN.Select(n => new { PLAN_ID = n.PLAN_ID, PRODUCT_ID = n.PRODUCT_ID, PHONE_NUMBER = n. });
            return Json(new { code = 0, msg = "", count = 1000, data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}

