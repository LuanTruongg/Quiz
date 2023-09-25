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
		}
	}
}
