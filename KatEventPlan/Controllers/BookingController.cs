using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using KatEventPlan.Models;

namespace KatEventPlan.Controllers
{
    public class BookingController : Controller
    {
        private EventDBContext db = new EventDBContext();

        // GET: Booking
        public ActionResult Index(string searchString)
        {
            var bookings = db.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(b =>
                    b.Event.Event_Name.Contains(searchString) ||
                    b.Venue.Venue_Name.Contains(searchString));
            }

            return View(bookings.ToList());
        }

        // GET: Booking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Booking booking = db.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefault(b => b.Booking_Id == id);

            if (booking == null)
                return HttpNotFound();

            return View(booking);
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            ViewBag.Event_Id = new SelectList(db.Events, "Event_Id", "Event_Name");
            ViewBag.Venue_Id = new SelectList(db.Venues, "Venue_Id", "Venue_Name");
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Booking_Id,Event_Id,Venue_Id,Booking_Date")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Event_Id = new SelectList(db.Events, "Event_Id", "Event_Name", booking.Event_Id);
            ViewBag.Venue_Id = new SelectList(db.Venues, "Venue_Id", "Venue_Name", booking.Venue_Id);
            return View(booking);
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Booking booking = db.Bookings.Find(id);
            if (booking == null)
                return HttpNotFound();

            ViewBag.Event_Id = new SelectList(db.Events, "Event_Id", "Event_Name", booking.Event_Id);
            ViewBag.Venue_Id = new SelectList(db.Venues, "Venue_Id", "Venue_Name", booking.Venue_Id);
            return View(booking);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Booking_Id,Event_Id,Venue_Id,Booking_Date")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Event_Id = new SelectList(db.Events, "Event_Id", "Event_Name", booking.Event_Id);
            ViewBag.Venue_Id = new SelectList(db.Venues, "Venue_Id", "Venue_Name", booking.Venue_Id);
            return View(booking);
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Booking booking = db.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefault(b => b.Booking_Id == id);

            if (booking == null)
                return HttpNotFound();

            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
