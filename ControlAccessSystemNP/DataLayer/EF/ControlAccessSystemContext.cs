using DataLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EF
{
	public class ControlAccessSystemContext : DbContext
	{
		public ControlAccessSystemContext() { }

        public ControlAccessSystemContext(DbContextOptions<ControlAccessSystemContext> opt) : base(opt) { }

		public DbSet<Administrator> administrators { get; set; }

		public DbSet<Worker> workers { get; set; }

		public DbSet<Card> cards { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Administrator>()
				.HasKey(a => a.Id);

			modelBuilder.Entity<Worker>()
				.HasKey(w => w.Id);

			modelBuilder.Entity<Worker>()
				.HasOne(w => w.Card)
				.WithOne(c => c.Worker)
				.HasForeignKey<Card>(c => c.WorkerId);

			modelBuilder.Entity<Card>()
				.HasKey(c => c.Id);
		}
	}
}
