using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Configuration
{
    public class UserSubjectConfiguration : IEntityTypeConfiguration<UserSubject>
    {
        public void Configure(EntityTypeBuilder<UserSubject> builder)
        {
            builder.ToTable("UserSubjects");
            builder.HasKey(x => new { x.UserId, x.SubjectId });
            builder.HasOne(x => x.User).WithMany(x => x.UserSubjects).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Subject).WithMany(x => x.UserSubjects).HasForeignKey(x => x.SubjectId);
        }
    }
}
