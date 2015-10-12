using System.Web.Mvc;
using RosterCheck_ASPNET.Models;

namespace RosterCheck_ASPNET.Controllers
{
    public class RosterController : Controller
    {
        // GET: Roster
        public ActionResult Roster()
        {
            ViewBag.message = GuildModel.GetGuildModel();

            var model = GuildModel.GetGuildModel();
            return View(model);
        }
    }
}