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
    public class EspecialistMap : IEntityTypeConfiguration<Especialist>
    {
        public void Configure(EntityTypeBuilder<Especialist> builder)
        {
            builder.ToTable("Especialist");
            builder.HasKey(c => c.EspecialistId);
            builder.Property(c => c.EspecialistId).HasColumnName("EspecialistId")
               .ValueGeneratedOnAdd();
            builder.Property(c => c.EspecialistName)
             .HasColumnName("EspecialistName")
             .HasMaxLength(30)
             .IsUnicode(false)
             .IsRequired();
            builder.Property(c => c.EspecialistLastName)
             .HasColumnName("EspecialistLastName")
             .HasMaxLength(30)
             .IsUnicode(false)
             .IsRequired();
            builder.Property(c => c.EspecialistEmail)
             .HasColumnName("EspecialistEmail")
             .HasMaxLength(50)
             .IsUnicode(false)
             .IsRequired();
            builder.Property(c => c.EspecialistPassword)
             .HasColumnName("EspecialistPassword")
             .HasMaxLength(50)
             .IsUnicode(false)
             .IsRequired();
            builder.Property(c => c.EspecialistInformation)
             .HasColumnName("EspecialistInformation")
             .HasMaxLength(100)
             .IsUnicode(false)
             .IsRequired();

        }
    }
}
