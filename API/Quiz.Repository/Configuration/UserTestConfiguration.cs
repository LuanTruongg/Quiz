using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Repository.Model;

namespace Quiz.Repository.Configuration
{
    public class UserTestConfiguration : IEntityTypeConfiguration<UserTest>
    {
        public void Configure(EntityTypeBuilder<UserTest> builder)
        {
            builder.ToTable("UserTests");
            builder.HasKey(x => x.UserTestId);
            builder.Property(x => x.UserTestId).IsRequired();
            builder.Property(x => x.Score).HasDefaultValue(0);
            builder.HasOne(x => x.User).WithMany(x => x.UserTests).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.TestStructure).WithMany(x => x.UserTest).HasForeignKey(x => x.TestStructureId);
        }
    }
}
