using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zalidane.com.Models;

namespace ZalidaneSite.Controllers
{
    public class SamplesController : Controller
    {
        //private ZalidaneBlogConnection db = new ZalidaneBlogConnection();
        private ZalidaneBlogModel db = new ZalidaneBlogModel();

        // GET: Samples
        public ActionResult Index()
        {
            List<CodeSample> codeSamples = null;
            codeSamples = db.CodeSamples.ToList();

            if (codeSamples == null)
            {
                codeSamples = new List<CodeSample>();
            }

            return View(codeSamples);
        }

        // GET: Samples/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeSample codeSample = db.CodeSamples.Find(id);
            if (codeSample == null)
            {
                return HttpNotFound();
            }
            return View(codeSample);
        }

        // GET: Samples/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Samples/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Url,Thumbnail,Image")] CodeSample codeSample, HttpPostedFileBase thumbnailUpload, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                /*
                if (thumbnailUpload != null && thumbnailUpload.ContentLength > 0)
                {
                    using (var reader = new BinaryReader(thumbnailUpload.InputStream))
                    {
                        codeSample.Thumbnail = reader.ReadBytes(thumbnailUpload.ContentLength);
                    }
                }

                if (imageUpload != null && imageUpload.ContentLength > 0)
                {
                    using (var reader = new BinaryReader(imageUpload.InputStream))
                    {
                        codeSample.Image = reader.ReadBytes(imageUpload.ContentLength);
                    }
                }
                */

                string message = "";
                try
                {
                    db.CodeSamples.Add(codeSample);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException dbe)
                {
                    foreach (var eve in dbe.EntityValidationErrors)
                    {
                        string msg = String.Format("Entity of Type {0} in state {1} has the following validation errors:\n\r",
                                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);

                        foreach (var ve in eve.ValidationErrors)
                        {
                            msg += String.Format("- Property: {0}, Error: {1}\n\r", ve.PropertyName, ve.ErrorMessage);
                        }

                        message += msg;
                    }

                    return View("Error", message);
                }

            }

            return View(codeSample);
        }

        // GET: Samples/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeSample codeSample = db.CodeSamples.Find(id);
            if (codeSample == null)
            {
                return HttpNotFound();
            }
            return View(codeSample);
        }

        // POST: Samples/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Url,Thumbnail,Image")] CodeSample codeSample, HttpPostedFileBase thumbnailUpload, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                /*
                if (thumbnailUpload != null && thumbnailUpload.ContentLength > 0)
                {
                    using (var reader = new BinaryReader(thumbnailUpload.InputStream))
                    {
                        codeSample.Thumbnail = reader.ReadBytes(thumbnailUpload.ContentLength);
                    }
                }

                if (imageUpload != null && imageUpload.ContentLength > 0)
                {
                    using (var reader = new BinaryReader(imageUpload.InputStream))
                    {
                        codeSample.Image = reader.ReadBytes(imageUpload.ContentLength);
                    }
                }
                */

                string message = "";
                try
                {
                    db.Entry(codeSample).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException dbe)
                {
                    foreach (var eve in dbe.EntityValidationErrors)
                    {
                        string msg = String.Format("Entity of Type {0} in state {1} has the following validation errors:\n\r",
                                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);

                        foreach (var ve in eve.ValidationErrors)
                        {
                            msg += String.Format("- Property: {0}, Error: {1}\n\r", ve.PropertyName, ve.ErrorMessage);
                        }

                        message += msg;
                    }

                    return View("Error", message);
                }
            }
            return View(codeSample);
        }

        // GET: Samples/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeSample codeSample = db.CodeSamples.Find(id);
            if (codeSample == null)
            {
                return HttpNotFound();
            }
            return View(codeSample);
        }

        // POST: Samples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodeSample codeSample = db.CodeSamples.Find(id);
            db.CodeSamples.Remove(codeSample);
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
    }
}
