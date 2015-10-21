using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RosterCheck_ASPNET.DAL;
using RosterCheck_ASPNET.Models;

namespace RosterCheck_ASPNET.Controllers
{
    public class CharacterController : Controller
    {
        private readonly GuildModelDbContext _db = new GuildModelDbContext();

        // GET: Character
        public ActionResult Index()
        {
            return View(_db.Characters.ToList());
        }

        // GET: Character/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuildModel guildModel = _db.GuildModels.Find(id);
            if (guildModel == null)
            {
                return HttpNotFound();
            }
            return View(guildModel);
        }

        // GET: Character/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Character/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LastModified,Name,Realm,Level,Side,AchievementPoints")] GuildModel guildModel)
        {
            if (ModelState.IsValid)
            {
                _db.GuildModels.Add(guildModel);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guildModel);
        }

        // GET: Character/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuildModel guildModel = _db.GuildModels.Find(id);
            if (guildModel == null)
            {
                return HttpNotFound();
            }
            return View(guildModel);
        }

        // POST: Character/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LastModified,Name,Realm,Level,Side,AchievementPoints")] GuildModel guildModel)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(guildModel).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guildModel);
        }

        // GET: Character/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuildModel guildModel = _db.GuildModels.Find(id);
            if (guildModel == null)
            {
                return HttpNotFound();
            }
            return View(guildModel);
        }

        // POST: Character/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GuildModel guildModel = _db.GuildModels.Find(id);
            _db.GuildModels.Remove(guildModel);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
