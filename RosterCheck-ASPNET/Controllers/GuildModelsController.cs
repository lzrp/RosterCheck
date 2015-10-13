using System.Web.Mvc;
using RosterCheck_ASPNET.Models;

namespace RosterCheck_ASPNET.Controllers
{
    public class GuildModelsController : Controller
    {
        // GET: Roster
        public ActionResult GuildModels()
        {
            ViewBag.message = GuildModel.GetGuildModel();

            var model = GuildModel.GetGuildModel();
            return View(model);
        }
    }
}