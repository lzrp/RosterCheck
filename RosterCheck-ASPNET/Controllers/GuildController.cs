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
            var guild = _dbContext.GuildModels.Find(realm, name);

            return guild != null ? View("Index", guild) : View("Index");
        }

        [HttpPost]
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

        //public ActionResult CachedGuildModels()
        //{
        //    var model = _dbContext.GuildModels.ToList().First();
        //    return View(model);
        //}

        //public ActionResult ImportGuild()
        //{
        //    //var importedGuild = GuildModel.GetGuildModel();

        //    //dbContext.GuildModels.AddOrUpdate(importedGuild);
        //    //dbContext.SaveChanges();

        //    return RedirectToAction("CachedGuildModels");
        //}
    }
}