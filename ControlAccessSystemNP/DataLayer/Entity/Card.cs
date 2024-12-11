namespace DataLayer.Entity
{
	public sealed class Card
	{
		public int Id { get; set; }

		public int EmployeeId { get; set; }

		public Worker Employee { get; set; }
	}
}
