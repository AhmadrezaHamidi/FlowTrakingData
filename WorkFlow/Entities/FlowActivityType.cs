using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositoryEfCore.Entities;

namespace WorkFlow.Entities
{
  

    public class FlowActivityType : EntityBase
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }

        public FlowActivityType(string name, string title, int priority)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Title = title;
            Priority = priority;
        }
    }
    public class FlowActivityTypeEntityTypeConfiguration : BaseEntityTypeConfiguration<FlowActivityType>
    {

        public void Configure(EntityTypeBuilder<FlowActivityType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValue("getDate()");
            builder.Property(x => x.UpdatedAt).HasDefaultValue(null);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Priority).IsRequired();

            builder.ToTable($"tbl{nameof(FlowActivityType)}");
        }
    }


}
