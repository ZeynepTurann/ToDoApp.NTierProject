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
    public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
    {
        public void Configure(EntityTypeBuilder<ToDo> builder)
        {
            builder.Property(x => x.Definition).IsRequired();
            builder.Property(x => x.Definition).HasMaxLength(200);

            builder.Property(x => x.Priority).IsRequired();

            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");

            

            builder.Property(x => x.StartDate).HasColumnType("date");
            builder.Property(x => x.Deadline).HasColumnType("date");
            builder.Property(x => x.FinishDate).HasColumnType("date");

      
        }
    }
}
