using Microsoft.EntityFrameworkCore;

namespace ApostlePath.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
