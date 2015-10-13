using System.Linq;
using System.Web.Mvc;
using RosterCheck_ASPNET.Models;

namespace RosterCheck_ASPNET.Controllers
{
    public class GuildModelsController : Controller
    {
        private GuildModel.GuildModelDbContext dbContext = new GuildModel.GuildModelDbContext();

        // GET: Roster
        public ActionResult GuildModels()
        {
            var model = GuildModel.GetGuildModel();
            return View(model);
        }

        public ActionResult CachedGuildModels()
        {
            var model = dbContext.GuildModels.ToList();
            return View(model);
        }
    }
}