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
    public class UserStructureConfiguration : IEntityTypeConfiguration<UserStructure>
    {
        public void Configure(EntityTypeBuilder<UserStructure> builder)
        {
            builder.ToTable("UserStructures");
            builder.HasKey(x => new { x.UserId, x.TestStructureId });
            builder.HasOne(x => x.User).WithMany(x => x.UserStructures).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.TestStructure).WithMany(x => x.UserStructures).HasForeignKey(x => x.TestStructureId);
        }
    }
}
