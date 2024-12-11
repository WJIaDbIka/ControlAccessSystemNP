namespace DataLayer.Entity
{
	public sealed class Worker
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public string Phone { get; set; } = string.Empty;

		public string Position { get; set; } = string.Empty;

		public Card Card { get; set; }
	}
}
