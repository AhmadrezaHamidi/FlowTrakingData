using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            Id = Guid.NewGuid();
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


    public interface IAggregateRoot
    {
    }

    public interface ITxRequest
    {
    }

    public abstract class EntityBase
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; }
        public bool IsDeleted { get; set; } = false;
    }


    public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<TBase> entityTypeBuilder)
        {
            entityTypeBuilder.Property<byte[]>("RowVersion").IsRowVersion();
        }
    }

}
