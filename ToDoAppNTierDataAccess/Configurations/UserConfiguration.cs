using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierEntities.Domains;

namespace ToDoAppNTierDataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(30);
            builder.HasIndex(x => x.UserName).IsUnique(true);

            builder.Property(x=>x.Password).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(20);

            builder.Property(x => x.Email).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique(true);

            builder.Ignore(x => x.UploadImage);

            builder.Property(x => x.RegisterDate).HasDefaultValueSql("getutcdate()");


            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.Property(x => x.PhoneNumber).HasDefaultValueSql("'Not Specified'");

            builder.Property(x=>x.Occupation).IsRequired();
            builder.Property(x => x.Occupation).HasDefaultValueSql("'Not Specified'");



            
            





        }
    }
}
