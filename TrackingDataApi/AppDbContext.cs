using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositoryEfCore.Entities;

namespace TrackingDataApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        //public DbSet<ThreadEntity> Threads{ get; set; }
        //public DbSet<ParticipantEntity> Participant { get; set; }
        //public DbSet<MessageEntity> Message { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
    public class UserEntity : EntityBase
    {
        public UserEntity(string email, string passWordHash, string passWordSalt, string phone)
        {
            Id = Guid.NewGuid().ToString();
            IsDeleted = false;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
            Email = email;
            PassWordHash = passWordHash;
            PassWordSalt = passWordSalt;
            Phone = phone;
        }

        public string Email { get; set; }
        public string PassWordHash { get; set; }
        public string PassWordSalt { get; set; }
        public string Phone { get; set; }
    }

    public class UserEntityTypeConfiguration : BaseEntityTypeConfiguration<UserEntity>
    {

        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).HasDefaultValue(false);
            builder.Property(x => x.PassWordHash).IsRequired();
            builder.Property(x => x.PassWordSalt).IsRequired();
            builder.Property(x => x.Phone).IsRequired();

            builder.ToTable($"tbl{nameof(UserEntity)}");
        }
    }
}
