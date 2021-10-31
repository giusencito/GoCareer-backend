using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gocareer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Gocareer.Infrastructure.Mapping
{
    public class WorkMap: IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.ToTable("Work");
            builder.HasKey(c => c.Workid);
            builder.Property(c => c.Workid)
               .HasColumnName("Workid")
               .ValueGeneratedOnAdd();

            builder.Property(c => c.WorkName)
                .HasColumnName("WorkName")
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(c => c.WorkDescription)
                .HasColumnName("WorkDescription")
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(c => c.Careerid)
               .HasColumnName("Careerid");

            builder.HasOne(c => c.Career)
                    .WithMany(c => c.Works)
                    .HasForeignKey(c => c.Careerid);

        }
    }
}
