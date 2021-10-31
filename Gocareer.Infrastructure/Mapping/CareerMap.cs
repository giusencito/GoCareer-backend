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
    public class CareerMap: IEntityTypeConfiguration<Career>
    {
        public void Configure(EntityTypeBuilder<Career> builder)
        {
            builder.ToTable("Career");
            builder.HasKey(c => c.Careerid);
            builder.Property(c => c.Careerid)
               .HasColumnName("Careerid")
               .ValueGeneratedOnAdd();

            builder.Property(c => c.CareerName)
                .HasColumnName("CareerName")
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(c => c.CareerDescription)
                .HasColumnName("CareerDescription")
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired();

        }


    }
}
