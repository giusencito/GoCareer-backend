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
    public class TestMap : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable("Test");
            builder.HasKey(c => c.Testid);
            builder.Property(c => c.Testid).HasColumnName("Testid")
               .ValueGeneratedOnAdd();
            builder.Property(c => c.Personalized)
            .HasColumnName("Personalized").IsRequired();

            builder.Property(c => c.EspecialistId)
           .HasColumnName("EspecialistId");
            builder.HasOne(c => c.Especialist).WithMany(i => i.Tests).HasForeignKey(c => c.EspecialistId);

            builder.Property(c => c.Testname)
                .HasColumnName("Testname")
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsRequired();

        }
    }
}
