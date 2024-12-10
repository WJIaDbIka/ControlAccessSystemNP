namespace DataLayer.Entity
{
	public sealed class Card
	{
		public int Id { get; set; }

		public int EmployeeId { get; set; }

		public Employee Employee { get; set; }
	}
}
