using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingDataApi;

namespace WorkFlow.Entities
{


    public class FlowType : EntityBase
    {
        public FlowType(
            string name, string title, int period)
        {
            Name = name;
            Title = title;
            Period = period;
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public int Period { get; set; }
    }



    public class FlowTypeEntityTypeConfiguration : BaseEntityTypeConfiguration<FlowType>
    {

        public void Configure(EntityTypeBuilder<FlowType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValue("getDate()");
            builder.Property(x => x.UpdatedAt).HasDefaultValue(null);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Period).IsRequired();

            builder.ToTable($"tbl{nameof(FlowType)}");
        }
    }
}
