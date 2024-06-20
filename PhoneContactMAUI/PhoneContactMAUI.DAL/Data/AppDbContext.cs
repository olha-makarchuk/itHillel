using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PhoneContactMAUI.DAL.Models;

namespace PhoneContactMAUI.DAL.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<PhoneContact> ContactsList { get; set; }
	}

	public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
	{
		AppDbContext IDesignTimeDbContextFactory<AppDbContext>.CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("DefaultConnection");

			var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
			optionsBuilder.UseSqlite(connectionString);

			return new AppDbContext(optionsBuilder.Options);
		}
	}
}
