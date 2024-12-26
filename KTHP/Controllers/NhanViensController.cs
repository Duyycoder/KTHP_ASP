using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KTHP.Models;

namespace KTHP.Controllers
{
    public class NhanViensController : Controller
    {
        private Management db = new Management();

        // GET: Nhanviens
        public ActionResult Index()
        {
            var nhanvien = db.Nhanvien.Include(n => n.Phongban);
            return View(nhanvien.ToList());
        }

        // GET: Nhanviens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanvien.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // GET: Nhanviens/Create
        public ActionResult Create()
        {
            ViewBag.maphong = new SelectList(db.Phongban, "maphong", "tenphong");
            return View();
        }

        // POST: Nhanviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "manv,hotennv,tuoi,diachi,luongnv,maphong,matkhau")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                db.Nhanvien.Add(nhanvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maphong = new SelectList(db.Phongban, "maphong", "tenphong", nhanvien.maphong);
            return View(nhanvien);
        }

        // GET: Nhanviens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanvien.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.maphong = new SelectList(db.Phongban, "maphong", "tenphong", nhanvien.maphong);
            return View(nhanvien);
        }

        // POST: Nhanviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "manv,hotennv,tuoi,diachi,luongnv,maphong,matkhau")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanvien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maphong = new SelectList(db.Phongban, "maphong", "tenphong", nhanvien.maphong);
            return View(nhanvien);
        }

        // GET: Nhanviens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanvien.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // POST: Nhanviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nhanvien nhanvien = db.Nhanvien.Find(id);
            db.Nhanvien.Remove(nhanvien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginProcess(string manv, string matkhau)
        {
            // Kiểm tra đăng nhập trong CSDL
            int maNV = int.Parse(manv);
            var nv = db.Nhanvien.FirstOrDefault(n => n.manv == maNV && n.matkhau == matkhau);
            if (nv != null)
            {
                Session["hoten"] = nv.hotennv;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu.";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
        public ActionResult PhongBan(int maphong)
        {
            var nhanviens = db.Nhanvien.Where(n => n.maphong == maphong).ToList();
            return View(nhanviens);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
