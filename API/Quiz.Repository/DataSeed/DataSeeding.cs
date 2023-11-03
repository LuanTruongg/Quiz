using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quiz.Repository.Model;

namespace Quiz.Repository.DataSeed
{
	public static class DataSeeding
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Department>().HasData(
				new Department() { DepartmentId = "SP&KHCB", Name = "Khoa Sư phạm và Khoa học cơ bản" },
				new Department() { DepartmentId = "KTL", Name = "Khoa Kinh tế - Luật" },
				new Department() { DepartmentId = "LLCTGDQP&TC", Name = "Khoa Lý luận chính trị - GD Quốc phòng và Thể chất" },
				new Department() { DepartmentId = "KTCN", Name = "Khoa Kỹ thuật Công nghệ" },
				new Department() { DepartmentId = "NN&CNTP", Name = "Khoa Nông nghiệp và Công nghệ Thực phẩm" }
				);
			modelBuilder.Entity<Major>().HasData(
				new Major() { MajorId = "CNTT", Name = "Công nghệ thông tin", DepartmentId = "KTCN" },
				new Major() { MajorId = "DK&TDH", Name = "Điều khiển và tự động hoá", DepartmentId = "KTCN" }
				);

            var roleTeacherId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC").ToString();
            var roleAdminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE").ToString();

            modelBuilder.Entity<IdentityRole>().HasData(
				new IdentityRole {
					Id = roleAdminId,
					Name = "Admin",
					NormalizedName = "Admin",
				},
                new IdentityRole
                {
                    Id = roleTeacherId,
                    Name = "Teacher",
                    NormalizedName = "Teacher",
                });

			var userId = new Guid("85A3EBB5-F310-4676-8D27-4D80C9C7FDEF").ToString();
            var hasher = new PasswordHasher<User>();
			modelBuilder.Entity<User>().HasData(
				new User()
				{
					Id = userId,
					UserName = "luantruong020302@gmail.com",
					Email = "luantruong020302@gmail.com",
					NormalizedUserName = "luantruong020302@gmail.com",
					NormalizedEmail = "luantruong020302@gmail.com,",
					PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = string.Empty,
                    Fullname = "Trương Hoàng Luân",
                    DoB = new DateTime(2002, 03, 02).Date,
					Address = "Bến Tre",
					CCCD = "202011237083",
					Money = 1000000,
					Sex = true,
					PhoneNumber = "0836151007"
                });
			modelBuilder.Entity<IdentityUserRole<string>>().HasData(
				new IdentityUserRole<string> { RoleId = roleTeacherId, UserId = userId },
				new IdentityUserRole<string> { RoleId = roleAdminId, UserId = userId });
        }
	}
}
