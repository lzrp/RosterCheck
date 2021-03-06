﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace RosterCheck_ASPNET.Models
{
    public class GuildModel
    {
        // properties
        public int Id { get; set; }
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
        public static string GetGuildJson(string realmName, string guildName)
        {
            try
            {
                // Create URL request
                var requestString =
                    $"https://eu.api.battle.net/wow/guild/{realmName}/{guildName}?fields=members&locale=en_GB&apikey={ConfigurationManager.AppSettings["API_KEY"]}";
                var request = WebRequest.Create(requestString);

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
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Deserializes a guild json string into a guild model.
        /// </summary>
        /// <returns>GuildModel object.</returns>
        public static GuildModel GetGuildModel(string realmName, string guildName)
        {
            try
            {
                // Deserialize the json guild info stream
                var deserializedGuild = JsonConvert.DeserializeObject<GuildModel>(GetGuildJson(realmName, guildName));

                return deserializedGuild;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public class Spec
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
            public string Icon { get; set; }
        }

        public class Character
        {
            private int _class;
            private int _race;

            public int Id { get; set; }
            public string Name { get; set; }
            public string Realm { get; set; }
            public int Class
            {
                get { return _class; }
                set
                {
                    _class = value;
                    ClassName = GetClassName(_class);
                } }
            public string ClassName { get; set; }

            public int Race
            {
                get { return _race; }
                set
                {
                    _race = value;
                    RaceName = GetRaceName(_race);
                }
            }

            public string RaceName { get; set; }
            public int Gender { get; set; }
            public int Level { get; set; }
            public Spec Spec { get; set; }
            public long LastModified { get; set; }
            public Audit Audit { get; set; } = new Audit();

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

            /// <summary>
            /// Returns a character object from the armory with the specified realm name and character name.
            /// </summary>
            /// <param name="realmName">The realm name on which the character resides</param>
            /// <param name="characterName">The name of the character.</param>
            /// <returns>Character object.</returns>
            public static Character GetCharacter(string realmName, string characterName)
            {
                try
                {
                    // Create URL request
                    var requestString =
                        $"https://eu.api.battle.net/wow/character/{realmName}/{characterName}?fields=items,audit&locale=en_GB&apikey={ConfigurationManager.AppSettings["API_KEY"]}";
                    var request = WebRequest.Create(requestString);

                    // Get the response
                    var response = request.GetResponse();

                    // Get the stream content returned by the server
                    var dataStream = response.GetResponseStream();

                    // Open the stream for reading, if there is no data in the stream, return null
                    if (dataStream == null) return new Character() {Name = "datastream error"};
                    var reader = new StreamReader(dataStream);

                    // Read the content of the stream
                    var responseString = reader.ReadToEnd();

                    // Cleanup
                    reader.Close();
                    response.Close();

                    // deserialize character json string and return a character object
                    var character = JsonConvert.DeserializeObject<Character>(responseString);

                    return character;
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public class Member
        {
            private int _rank;

            public int Id { get; set; }
            public Character Character { get; set; }
            public int Rank
            {
                get { return _rank; }
                set
                {
                    _rank = value;
                    RankName = GetRankName(_rank);
                }
            }

            public string RankName { get; set; }

            /// <summary>
            /// Assigns a guild rank name for the specified guild rank ID.
            /// </summary>
            /// <param name="rank">Guild rank ID queried from armory.</param>
            /// <returns>Guild ranke name string.</returns>
            public static string GetRankName(int rank)
            {
                switch (rank)
                {
                    case 0:
                        return "GM";
                    case 1:
                        return "r1";
                    case 2:
                        return "r2";
                    case 3:
                        return "r3";
                    case 4:
                        return "r4";
                    case 5:
                        return "r5";
                    case 6:
                        return "r6";
                    case 7:
                        return "alt";
                    case 8:
                        return "lolrank";
                    default:
                        return "unknown guild rank";
                }
            }

            public class Emblem
            {
                public int Icon { get; set; }
                public string IconColor { get; set; }
                public int Border { get; set; }
                public string BorderColor { get; set; }
                public string BackgroundColor { get; set; }
            }

            // DbContext for GuildModel class
            
        }

        public class Audit
        {
            public int NumberOfIssues { get; set; } = 0;
            public int EmptySockets { get; set; } = 0;
        }


    }
}