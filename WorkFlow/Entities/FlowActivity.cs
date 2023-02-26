using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingDataApi;
using WorkFlow.Entities;

namespace WorkFlow.Entities
{

    public class FlowActivity
    {
        public FlowActivity(
            string pervioseUniqeId,
            string flowId, int flowActivityTypeId, string flowActivityTypeName,
            string flowActivityTypeTitle, int priority)
        {
            Id = Guid.NewGuid().ToString();
            PervioseUniqeId = pervioseUniqeId;
            CreatedAt = DateTime.Now;
            FlowId = flowId;
            FlowActivityTypeId = flowActivityTypeId;
            FlowActivityTypeName = flowActivityTypeName;
            FlowActivityTypeTitle = flowActivityTypeTitle;
            Priority = priority;
        }

        public string Id { get; set; }
        public int PervioseId { get; set; }

        public string PervioseUniqeId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string FlowId { get; set; }

        public string FlowUniqeId { get; set; }

        public int FlowActivityTypeId { get; set; }

        public string FlowActivityTypeName { get; set; }

        public string FlowActivityTypeTitle { get; set; }

        public int Priority { get; set; }
    }
}


public class FlowEntityTypeConfiguration : BaseEntityTypeConfiguration<Flow>
{

    public void Configure(EntityTypeBuilder<Flow> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedAt).HasDefaultValue("getDate()");
        builder.Property(x => x.UpdatedAt).HasDefaultValue(null);
        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        builder.Property(x => x.Version).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.FlowTypeId).IsRequired();
        builder.Property(x => x.FlowTypeName).IsRequired();
        builder.Property(x => x.FlowTypeTitle).IsRequired();

        builder.ToTable($"tbl{nameof(Flow)}");
    }
}





