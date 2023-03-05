using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositoryEfCore.Entities;
using WorkFlow.Entities;

namespace WorkFlow.Entities
{

    public class FlowActivity : EntityBase
    {
        public FlowActivity(
            string pervioseId,
            string flowId, string flowActivityTypeId,
            string flowActivityTypeName,
            string flowActivityTypeTitle, int priority)
        {
            Id = Guid.NewGuid().ToString();
            PervioseId = pervioseId;
            CreatedAt = DateTime.Now;
            FlowId = flowId;
            FlowActivityTypeId = flowActivityTypeId;
            FlowActivityTypeName = flowActivityTypeName;
            FlowActivityTypeTitle = flowActivityTypeTitle;
            Priority = priority;
        }

        public string PervioseId { get; set; }
        public string FlowId { get; set; }
        public string FlowActivityTypeId { get; set; }
        public string FlowActivityTypeName { get; set; }
        public string FlowActivityTypeTitle { get; set; }
        public int Priority { get; set; }
    }
}

public class FlowActivityEntityTypeConfiguration : BaseEntityTypeConfiguration<FlowActivity>
{

    public void Configure(EntityTypeBuilder<FlowActivity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedAt).HasDefaultValue("getDate()");
        builder.Property(x => x.UpdatedAt).HasDefaultValue(null);
        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        builder.Property(x => x.PervioseId);
        builder.Property(x => x.FlowId).IsRequired();
        builder.Property(x => x.FlowActivityTypeId).IsRequired();
        builder.Property(x => x.FlowActivityTypeName).IsRequired();
        builder.Property(x => x.FlowActivityTypeTitle).IsRequired();
        builder.Property(x => x.Priority).IsRequired();

        builder.ToTable($"tbl{nameof(Flow)}");
    }
}





