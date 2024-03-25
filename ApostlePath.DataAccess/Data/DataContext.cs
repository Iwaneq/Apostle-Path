using ApostlePath.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace ApostlePath.DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Quest> Quests { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

        public DataContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public async Task SeedData()
        {
            if (Quests.Count() != 0) return;

            Quests.Add(new Quest() { Title = "Cold Shower", Experience = 2, Level = 1, Challenge = "I will spend one more minute in my cold shower today", LastProgress = DateTime.UtcNow.AddDays(-1).Date, LastChecked = DateTime.UtcNow.AddDays(-1).Date });
            Quests.Add(new Quest() { Title = "Gym", Experience = 4, Level = 2, Challenge = "I will start with 2 minutes of cardio today", LastProgress = DateTime.UtcNow.AddDays(-1).Date, LastChecked = DateTime.UtcNow.AddDays(-1).Date });
            Quests.Add(new Quest() { Title = "Reading Books", Experience = 6, Level = 6, Challenge = "I will read one page more today", LastProgress = DateTime.UtcNow.AddDays(-1).Date, LastChecked = DateTime.UtcNow.AddDays(-1).Date });
            await SaveChangesAsync();
        }
    }
}
