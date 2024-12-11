using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataLayer.EF
{
	public sealed class ControlAccessSystemContextFactory : IDesignTimeDbContextFactory<ControlAccessSystemContext>
	{
		public ControlAccessSystemContext CreateDbContext(string[] args)
		{
			var optBuilder = new DbContextOptionsBuilder<ControlAccessSystemContext>();
			var connectionString = Environment.GetEnvironmentVariable("Connection");
			optBuilder.UseNpgsql(connectionString);

			return new ControlAccessSystemContext(optBuilder.Options);
		}
	}
}
