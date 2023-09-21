using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Repository.Model;

namespace Quiz.Repository.Configuration
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("Modules");
            builder.HasKey(x => x.ModuleId);
            builder.Property(x => x.ModuleId).IsRequired();
            builder.HasOne(x => x.Subject).WithMany(x => x.Modules).HasForeignKey(x => x.SubjectId);
        }
    }
}
