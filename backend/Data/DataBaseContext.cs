using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }
    }
}
