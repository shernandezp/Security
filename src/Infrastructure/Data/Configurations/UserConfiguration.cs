﻿using Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Common.Domain.Constants;

namespace Security.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //Table name
        builder.ToTable(name: TableMetadata.User, schema: SchemaMetadata.Security);

        //Column names
        builder.Property(x => x.UserId).HasColumnName("id");
        builder.Property(x => x.Username).HasColumnName("username");
        builder.Property(x => x.Password).HasColumnName("password");
        builder.Property(x => x.FirstName).HasColumnName("firstname");
        builder.Property(x => x.SecondName).HasColumnName("secondname");
        builder.Property(x => x.LastName).HasColumnName("lastname");
        builder.Property(x => x.SeconSurname).HasColumnName("secondsurname");
        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.DOB).HasColumnName("dob");
        builder.Property(x => x.Active).HasColumnName("active");
        builder.Property(x => x.AccountId).HasColumnName("accountid");

        builder.Property(t => t.Username)
            .HasMaxLength(ColumnMetadata.DefaultUserNameLength)
            .IsRequired();

        builder.Property(t => t.Password)
            .HasMaxLength(ColumnMetadata.DefaultPasswordLength)
            .IsRequired();

        builder.Property(t => t.Email)
            .HasMaxLength(ColumnMetadata.DefaultEmailLength)
            .IsRequired();

        //Constraints
        builder
            .HasMany(e => e.Roles)
            .WithMany(e => e.Users)
            .UsingEntity<UserRole>();

        builder
            .HasMany(e => e.Profiles)
            .WithMany(e => e.Users)
            .UsingEntity<UserProfile>();

    }
}
