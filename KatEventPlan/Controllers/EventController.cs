using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity;
using KatEventPlan.Models;

namespace KatEventPlan.Controllers
{
    public class EventController : Controller
    {
        private EventDBContext db = new EventDBContext();

        // GET: Event
        public ActionResult Index(string searchString)
        {
            var events = db.Events.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.Event_Name.Contains(searchString) || e.Description.Contains(searchString));
            }

            return View(events.ToList());
        }


        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var eventModel = db.Events.Include(e => e.Venue).FirstOrDefault(e => e.Event_Id == id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }

            return View(eventModel);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            ViewBag.Venue_Id = new SelectList(db.Venues, "Venue_Id", "Venue_Name");
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Event_Id,Event_Name,Event_Date,Description,Venue_Id")] Event eventModel)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(eventModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Venue_Id = new SelectList(db.Venues, "Venue_Id", "Venue_Name", eventModel.Venue_Id);
            return View(eventModel);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var eventModel = db.Events.Find(id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.Venue_Id = new SelectList(db.Venues, "Venue_Id", "Venue_Name", eventModel.Venue_Id);
            return View(eventModel);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Event_Id,Event_Name,Event_Date,Description,Venue_Id")] Event eventModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Venue_Id = new SelectList(db.Venues, "Venue_Id", "Venue_Name", eventModel.Venue_Id);
            return View(eventModel);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var eventModel = db.Events.Include(e => e.Venue).FirstOrDefault(e => e.Event_Id == id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }

            return View(eventModel);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var eventModel = db.Events.Find(id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }

            db.Events.Remove(eventModel);
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
