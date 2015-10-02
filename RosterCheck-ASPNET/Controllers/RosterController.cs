using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Newtonsoft.Json;
using RosterCheck_ASPNET.Models;

namespace RosterCheck_ASPNET.Controllers
{
    public class RosterController : Controller
    {
        // GET: Roster
        public ActionResult Roster()
        {
            ViewBag.message = RosterController.GetGuildModel();

            var model = GetGuildModel();
            return View(model);
        }

        private static string GetGuildJson()
        {
            try
            {
                // Create URL request
                var request = WebRequest.Create("https://eu.api.battle.net/" +
                                                "wow/guild/the-maelstrom/" +
                                                "Project%20flying%20monkey?fields=members&locale=en_GB&apikey=" +
                                                ConfigurationManager.AppSettings["API_KEY"]);

                // Get the response
                var response = request.GetResponse();

                // Get the stream content returned by the server
                var dataStream = response.GetResponseStream();

                // Open the stream for reading
                if (dataStream != null)
                {
                    var reader = new StreamReader(dataStream);

                    // Read the content of the stream
                    var responseString = reader.ReadToEnd();

                    // Cleanup
                    reader.Close();
                    response.Close();

                    return responseString;
                }

                return "";
            }
            catch (Exception)
            {
                throw;
            }

        }

        private static GuildModel GetGuildModel()
        {
            try
            {
                // Deserialize the json guild info stream
                var deserializedGuild = JsonConvert.DeserializeObject<GuildModel>(GetGuildJson());

                // Assign class names to members from their class id
                foreach (var member in deserializedGuild.Members)
                {
                    member.Character.ClassName = Enum.GetName(typeof (Character.ClassCode), member.Character.Class);
                }

                return deserializedGuild;
            }
            catch (Exception)
            {
                throw;
                
            }
        }
    }
}