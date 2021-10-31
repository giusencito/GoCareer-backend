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
    public class OptionMap : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.ToTable("Option");
            builder.HasKey(c => c.Optionid);
            builder.Property(c => c.Optionid).HasColumnName("Optionid")
               .ValueGeneratedOnAdd();

            builder.Property(c => c.OptionName)
             .HasColumnName("OptionName")
             .HasMaxLength(50)
             .IsUnicode(false)
             .IsRequired();

            //builder.Property(c => c.Correct)
            //  .HasColumnName("Correct").IsRequired();

            builder.Property(c => c.Points)
              .HasColumnName("Points").IsRequired();

            builder.Property(c => c.QuestionId)
            .HasColumnName("QuestionId");

            builder.HasOne(c => c.Question).WithMany(i => i.Options).HasForeignKey(c => c.QuestionId);


        }
    }
}
