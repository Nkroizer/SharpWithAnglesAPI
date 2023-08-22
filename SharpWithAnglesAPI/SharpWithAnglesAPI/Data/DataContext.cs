using Microsoft.EntityFrameworkCore;

namespace SharpWithAnglesAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Animal> Animals => Set<Animal>();
    }
}
