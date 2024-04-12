using Capstone_api.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone_api.Utility
{
    public partial class GlobalDBContext : DbContext
    {
        public GlobalDBContext(DbContextOptions<GlobalDBContext> options) 
            : base(options) { }

        public virtual DbSet<Person> Person { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(k => k.Id);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
