using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace RosterCheck_ASPNET.Models
{
    public class GuildModel
    {
        // properties
        public long LastModified { get; set; }
        public string Name { get; set; }
        public string Realm { get; set; }
        public int Level { get; set; }
        public int Side { get; set; }
        public int AchievementPoints { get; set; }
        public List<Member> Members { get; set; }

        /// <summary>
        /// Queries the armory for a json string of a guild.
        /// </summary>
        /// <returns>Json string.</returns>
        public static string GetGuildJson()
        {
            try
            {
                // Create URL request
                var request = WebRequest.Create("https://eu.api.battle.net/" +
                                                "wow/guild/the-maelstrom/" +
                                                "Project%20flying%20monkey" +
                                                "?fields=members&locale=en_GB&apikey=" +
                                                ConfigurationManager.AppSettings["API_KEY"]);

                // Get the response
                var response = request.GetResponse();

                // Get the stream content returned by the server
                var dataStream = response.GetResponseStream();

                // Open the stream for reading, if there is no data in the stream, return null
                if (dataStream == null) return "";
                var reader = new StreamReader(dataStream);

                // Read the content of the stream
                var responseString = reader.ReadToEnd();

                // Cleanup
                reader.Close();
                response.Close();

                return responseString;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Deserializes a guild json string into a guild model.
        /// </summary>
        /// <returns>GuildModel object.</returns>
        public static GuildModel GetGuildModel()
        {
            try
            {
                // Deserialize the json guild info stream
                var deserializedGuild = JsonConvert.DeserializeObject<GuildModel>(GetGuildJson());

                // Assign class and race names to members
                foreach (var member in deserializedGuild.Members)
                {
                    member.Character.ClassName = member.Character.GetClassName(member.Character.Class);
                    member.Character.RaceName = member.Character.GetRaceName(member.Character.Race);
                }
                
                return deserializedGuild;
            }
            catch (Exception)
            {
                throw;

            }
        }

        public class Spec
        {
            public string Name { get; set; }
            public string Role { get; set; }
            public string Icon { get; set; }
        }

        public class Character
        {
            [DisplayName("Name")]
            public string Name { get; set; }
            public string Realm { get; set; }
            public int Class { get; set; }
            public string ClassName { get; set; }
            public int Race { get; set; }
            public string RaceName { get; set; }
            public int Gender { get; set; }
            public int Level { get; set; }
            public Spec Spec { get; set; }
            public int LastModified { get; set; }

            /// <summary>
            /// Returns a class name based on the class id.
            /// </summary>
            /// <param name="classId">The class id queried from the armory.</param>
            /// <returns>Class name string.</returns>
            public string GetClassName(int classId)
            {
                switch (classId)
                {
                    case 1:
                        return "Warrior";
                    case 2:
                        return "Paladin";
                    case 3:
                        return "Hunter";
                    case 4:
                        return "Rogue";
                    case 5:
                        return "Priest";
                    case 6:
                        return "Death Knight";
                    case 7:
                        return "Shaman";
                    case 8:
                        return "Mage";
                    case 9:
                        return "Warlock";
                    case 10:
                        return "Monk";
                    case 11:
                        return "Druid";
                    default:
                        return "unknown class";
                }
            }

            /// <summary>
            /// Returns a race name based on the race id.
            /// </summary>
            /// <param name="raceId">Race id queried from the armory.</param>
            /// <returns>Race name string.</returns>
            public string GetRaceName(int raceId)
            {
                switch (raceId)
                {
                    case 1:
                        return "Human";
                    case 2:
                        return "Orc";
                    case 3:
                        return "Dwarf";
                    case 4:
                        return "Night Elf";
                    case 5:
                        return "Undead";
                    case 6:
                        return "Tauren";
                    case 7:
                        return "Gnome";
                    case 8:
                        return "Troll";
                    case 9:
                        return "Goblin";
                    case 10:
                        return "Blood Elf";
                    case 11:
                        return "Draenei";
                    case 22:
                        return "Worgen";
                    case 25:
                        return "Pandaren";
                    case 26:
                        return "Pandaren";
                    default:
                        return "unknown race";
                }
            }
        }

        public class Member
        {
            public Character Character { get; set; }
            public int Rank { get; set; }
        }

        public class Emblem
        {
            public int Icon { get; set; }
            public string IconColor { get; set; }
            public int Border { get; set; }
            public string BorderColor { get; set; }
            public string BackgroundColor { get; set; }
        }


    }
}