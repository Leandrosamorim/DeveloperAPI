using Data.Mapping;
using Domain.ContactNS;
using Domain.DeveloperNS;
using Domain.ProgrammingStackNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Context
{
    public class DeveloperDBContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DeveloperDBContext(DbContextOptions<DeveloperDBContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public virtual DbSet<Developer> Developer { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ProgrammingStack> ProgrammingStack { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProgrammingStackMapping());
            builder.Entity<Developer>()
                .HasIndex(u => u.Login)
                .IsUnique();
        }
    }
}
