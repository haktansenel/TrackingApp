using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TrackingApp.Core.Enums;

namespace TrackingApp.Core.Entites
{
    public class User : BaseEntity
    {

        public string UserName { get; set; }

        [NotMapped]
        public string Password { get; set; }

        public string NameSurname { get; set; }
        public string EncryptedPassword { get; set; }
        public string Email { get; set; }

        public UserType UserType { get; set; }


    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("User");

            #region  Property Configurations
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
            builder.Property(x => x.EncryptedPassword).IsRequired().HasMaxLength(500);
            builder.Property(x => x.UserType).IsRequired().HasConversion<int>();
            #endregion

            #region Constraints
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserName).IsUnique();  
            builder.HasIndex(x => x.Email).IsUnique();  
            #endregion


        }
    }
}
