using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RosterCheck_ASPNET.Models;

namespace RosterCheck_ASPNET.DAL
{
    public class GuildModelDbContext : DbContext
    {
        public DbSet<GuildModel> GuildModels { get; set; }
        public DbSet<GuildModel.Member> Members { get; set; }
        public DbSet<GuildModel.Character> Characters { get; set; }
    }
}