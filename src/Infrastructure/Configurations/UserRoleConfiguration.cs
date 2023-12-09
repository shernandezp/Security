﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Common.Domain.Constants;

namespace Security.Infrastructure.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        //Table name
        builder.ToTable(name: TableMetadata.UserRole, schema: SchemaMetadata.Security);

        //Column names
        builder.Property(x => x.UserId).HasColumnName("userid");
        builder.Property(x => x.RoleId).HasColumnName("roleid");

    }
}
