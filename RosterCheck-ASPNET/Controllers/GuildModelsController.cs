using System.Linq;
using System.Net;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using RosterCheck_ASPNET.Models;
using RosterCheck_ASPNET.DAL;

namespace RosterCheck_ASPNET.Controllers
{
    public class GuildModelsController : Controller
    {
        private GuildModelDbContext dbContext = new GuildModelDbContext();

        // GET: Roster
        public ActionResult GuildModels()
        {
            //if (realmName == null || guildName == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //var model = GuildModel.GetGuildModel(realmName, guildName);
            return View();
        }

        public ActionResult CachedGuildModels()
        {
            var model = dbContext.GuildModels.ToList().First();
            return View(model);
        }

        public ActionResult ImportGuild()
        {
            //var importedGuild = GuildModel.GetGuildModel();

            //dbContext.GuildModels.Add(importedGuild);
            //dbContext.SaveChanges();

            return RedirectToAction("CachedGuildModels");
        }
    }
}