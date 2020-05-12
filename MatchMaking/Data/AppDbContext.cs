using MatchMaking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchMaking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        

        public DbSet<Player> OnlinePlayers { get; set; }
        public DbSet<Server> ActiveServers { get; set; }
    }
}
