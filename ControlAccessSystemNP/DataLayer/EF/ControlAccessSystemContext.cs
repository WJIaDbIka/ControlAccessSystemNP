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

			modelBuilder.Entity<Administrator>()
				.HasOne(a => a.Card)
				.WithOne()
				.HasForeignKey<Administrator>(a => a.CardId);

			modelBuilder.Entity<Worker>()
				.HasKey(w => w.Id);

			modelBuilder.Entity<Worker>()
				.HasOne(w => w.Card)
				.WithOne()
				.HasForeignKey<Worker>(w => w.CardId);

			modelBuilder.Entity<Card>()
				.HasKey(c => c.Id);

			modelBuilder.Entity<Card>()
				.HasOne(c => c.Employee)
				.WithOne()
				.HasForeignKey<Card>(c => c.EmployeeId);
		}
	}
}
