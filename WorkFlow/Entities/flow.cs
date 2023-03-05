﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RepositoryEfCore.Entities;

namespace WorkFlow.Entities;


[Table("Flow")]
public class Flow : EntityBase
{
    public Flow(
        string version, string userId,
        string flowTypeId, string flowTypeName,
        string flowTypeTitle)
    {
        Id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.Now;
        Version = version;
        UserId = userId;
        FlowTypeId = flowTypeId;
        FlowTypeName = flowTypeName;
        FlowTypeTitle = flowTypeTitle;
    }
    public string Version { get; set; }
    public string UserId { get; set; }
    public string FlowTypeId { get; set; }
    public string FlowTypeName { get; set; }
    public string FlowTypeTitle { get; set; }
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

