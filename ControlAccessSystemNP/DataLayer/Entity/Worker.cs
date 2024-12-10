namespace DataLayer.Entity
{
	public sealed class Worker : Employee
	{
		public int CardId { get; set; }

		public Card Card { get; set; }
	}
}
