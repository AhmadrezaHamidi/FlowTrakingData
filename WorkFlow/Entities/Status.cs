using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingDataApi;

namespace WorkFlow.Entities
{

    public class Status : EntityBase
    {
        public Status( 
            string statusTypeId, string statusTypeName,
            string statusTypeTitle, string flowActivityId)
        {
            Id  = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            StatusTypeId = statusTypeId;
            StatusTypeName = statusTypeName;
            StatusTypeTitle = statusTypeTitle;
            FlowActivityId = flowActivityId;
        }
        public string StatusTypeId { get; set; }
        public string StatusTypeName { get; set; }
        public string StatusTypeTitle { get; set; }
        public string FlowActivityId { get; set; }
    }
    public class StatusEntityTypeConfiguration : BaseEntityTypeConfiguration<Status>
    {

        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValue("getDate()");
            builder.Property(x => x.UpdatedAt).HasDefaultValue(null);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.StatusTypeId).IsRequired();
            builder.Property(x => x.StatusTypeName).IsRequired();
            builder.Property(x => x.StatusTypeTitle).IsRequired();
            builder.Property(x => x.FlowActivityId).IsRequired();

            builder.ToTable($"tbl{nameof(Status)}");
        }
    }


}
