using System.Linq;
using System.Net;
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

        [HttpPost]
        public ActionResult Index(GuildModel guildSearchModel)
        {
            // Check for blank strings in the search query
            if (guildSearchModel.Realm == null || guildSearchModel.Name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = GuildModel.GetGuildModel(guildSearchModel.Realm, guildSearchModel.Name);

            //return View("CachedGuildModels", model);
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