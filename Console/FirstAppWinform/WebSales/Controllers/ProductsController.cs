using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSales.Models.EF;
using WebSales.Models.DAO;

namespace WebSales.Controllers
{
    public class ProductsController : BaseController
    {
        private ProductDAO dao = new ProductDAO();
        // GET: Products
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10, string keyword = "")
        {
            var model = await dao.GetByPaged(page, pageSize, keyword);

            ViewBag.Keyword = keyword;
            ViewBag.Page = page;
            ViewBag.Pagesize = pageSize;

            return View(model);
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await dao.GetSingleByID((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public async Task<ActionResult> Create()
        {
            Product model = new Product()
            {
                ProductDate = DateTime.Now,
            };

            ViewBag.CategoryID = new SelectList(await new CategoryDAO().GetAll(), "ID", "Name");
            ViewBag.Available = GetSelectListAvailabel(false, false);
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,UnitPrice,Image,ProductDate,Available,CategoryID,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (await dao.Add(product))
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CategoryID = new SelectList(await new CategoryDAO().GetAll(), "ID", "Name", product.CategoryID);
            ViewBag.Available = GetSelectListAvailabel(false, (bool)product.Available);

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await dao.GetSingleByID((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(await new CategoryDAO().GetAll(), "ID", "Name", product.CategoryID);
            ViewBag.Available = GetSelectListAvailabel(false, (bool)product.Available);

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,UnitPrice,Image,ProductDate,Available,CategoryID,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (await dao.Update(product))
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CategoryID = new SelectList(await new CategoryDAO().GetAll(), "ID", "Name", product.CategoryID);
            ViewBag.Available = GetSelectListAvailabel(false, (bool)product.Available);

            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await dao.GetSingleByID((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (await dao.Delete(id))
            {
                return RedirectToAction("Index");
            }

            Product product = await dao.GetSingleByID((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        //db.dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private SelectList GetSelectListAvailabel(bool isSelectedValue, bool val)
        {
            List<object> objects = new List<object>()
            {
                new {Name = "Con ban", value = true},
                new {Name = "Ngung ban", value = false},
            };

            if (isSelectedValue)
            {
                return new SelectList(objects, "Value", "Name", val);

            }
            return new SelectList(objects, "Value", "Name");

        }
    }
}
