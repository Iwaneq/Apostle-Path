using ApostlePath.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace ApostlePath.DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Quest> Quests { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
