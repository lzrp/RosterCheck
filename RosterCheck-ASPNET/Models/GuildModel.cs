using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RosterCheck_ASPNET.Models
{
    public class GuildModel
    {
        public long LastModified { get; set; }
        public string Name { get; set; }
        public string Realm { get; set; }
        public int Level { get; set; }
        public int Side { get; set; }
        public int AchievementPoints { get; set; }
        public List<Member> Members { get; set; }
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
        public int Gender { get; set; }
        public int Level { get; set; }
        public Spec Spec { get; set; }
        public int LastModified { get; set; }
        public enum ClassCode
        {
            Warrior = 1,
            Paladin = 2,
            Hunter = 3,
            Rogue = 4,
            Priest = 5,
            DeathKnight = 6,
            Shaman = 7,
            Mage = 8,
            Warlock = 9,
            Monk = 10,
            Druid = 11
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