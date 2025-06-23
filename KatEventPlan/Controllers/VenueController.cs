using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Azure.Storage.Blobs;
using KatEventPlan.Models;

namespace KatEventPlan.Controllers
{
    public class VenueController : Controller
    {
        private EventDBContext db = new EventDBContext();

        private readonly string storageConnection = "DefaultEndpointsProtocol=https;AccountName=kateventstorage;AccountKey=9oWhE1ViOnCsy5OD+OEJc0wWbdeihakx995VhTlam8YQhpVCeajUhiKa0G31cHyWrUKgRkIaA6ox+AStID4+ZQ==;EndpointSuffix=core.windows.net";
        private readonly string containerName = "kateventpictures";

        private async Task UploadImageToBlobAsync(HttpPostedFileBase file, int venueId)
        {
            var blobServiceClient = new BlobServiceClient(storageConnection);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            await containerClient.CreateIfNotExistsAsync();
            await containerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            // Use venueId for consistent naming
            string fileName = $"venue_{venueId}" + System.IO.Path.GetExtension(file.FileName);
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = file.InputStream)
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }
        }


        // GET: Venue
        public ActionResult Index()
        {
            return View(db.Venues.ToList());
        }

        // GET: Venue/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var venue = db.Venues.Find(id);
            if (venue == null)
                return HttpNotFound();

            return View(venue);
        }

        // GET: Venue/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Venue/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Venue venue, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                db.Venues.Add(venue);
                db.SaveChanges(); // Save to generate Venue_Id

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    await UploadImageToBlobAsync(imageFile, venue.Venue_Id); // ✅ Pass venue ID here
                }

                return RedirectToAction("Index");
            }

            return View(venue);
        }


        // GET: Venue/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var venue = db.Venues.Find(id);
            if (venue == null)
                return HttpNotFound();

            return View(venue);
        }

        // POST: Venue/Edit/5
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Edit(Venue venue, HttpPostedFileBase imageFile)
{
    if (ModelState.IsValid)
    {
        db.Entry(venue).State = EntityState.Modified;
        db.SaveChanges(); // Save updated details

        if (imageFile != null && imageFile.ContentLength > 0)
        {
            await UploadImageToBlobAsync(imageFile, venue.Venue_Id); // ✅ Pass venue ID here too
        }

        return RedirectToAction("Index");
    }

    return View(venue);
}


        // GET: Venue/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var venue = db.Venues.Find(id);
            if (venue == null)
                return HttpNotFound();

            return View(venue);
        }

        // POST: Venue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var venue = db.Venues.Find(id);
            db.Venues.Remove(venue);
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
