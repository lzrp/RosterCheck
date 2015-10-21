using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using RosterCheck_ASPNET.Models;
using RosterCheck_ASPNET.DAL;

namespace RosterCheck_ASPNET.Controllers
{
    public class GuildController : Controller
    {
        private readonly GuildModelDbContext _dbContext = new GuildModelDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        public ActionResult IndexFromDb(string realm, string name)
        {
            // Looks up a guild in the db by realm and guild name
            // linq is used for eagerly loading all related entities
            var guild = _dbContext.GuildModels
                .Include(x => x.Members.Select(c => c.Character).Select(v => v.Spec))
                .SingleOrDefault(g => g.Realm == realm && g.Name == name);

            // return a populated view when found or a blank one if no such guild exists in the db
            if (guild != null)
            {
                return View("Index", guild);
            }

            //TODO display an alert - "no guilds found"
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(GuildModel guildSearchModel)
        {
            // Check for blank strings in the search query
            if (guildSearchModel.Realm == null || guildSearchModel.Name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // get the guild from armory
            var model = GuildModel.GetGuildModel(guildSearchModel.Realm, guildSearchModel.Name);

            // adds or updates the guild in the database
            _dbContext.GuildModels.AddOrUpdate(x => new { x.Realm, x.Name},model);
            _dbContext.SaveChanges();

            return View(model);
        }

        // testing some stuff
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}