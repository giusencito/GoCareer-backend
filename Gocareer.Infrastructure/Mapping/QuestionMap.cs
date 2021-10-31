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
    public class QuestionMap : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Question");
            builder.HasKey(c => c.QuestionId);
            builder.Property(c => c.QuestionId).HasColumnName("QuestionId")
               .ValueGeneratedOnAdd();

            builder.Property(c => c.QuestionName)
              .HasColumnName("QuestionName")
              .HasMaxLength(50)
              .IsUnicode(false)
              .IsRequired();
            builder.Property(c => c.Score)
              .HasColumnName("Score").IsRequired();

            builder.Property(c => c.Testid)
            .HasColumnName("Testid");

            builder.HasOne(c => c.Test).WithMany(i => i.Questions).HasForeignKey(c => c.Testid);
        }
    }
}
