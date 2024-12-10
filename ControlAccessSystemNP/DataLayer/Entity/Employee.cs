namespace DataLayer.Entity
{
	public abstract class Employee
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public string Phone { get; set; } = string.Empty;

		public string Position { get; set; } = string.Empty;
	}
}
