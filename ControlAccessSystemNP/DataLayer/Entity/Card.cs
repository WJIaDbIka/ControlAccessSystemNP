namespace DataLayer.Entity
{
	public sealed class Card
	{
		public int Id { get; set; }

		public int WorkerId { get; set; }

		public Worker Worker { get; set; }
	}
}
