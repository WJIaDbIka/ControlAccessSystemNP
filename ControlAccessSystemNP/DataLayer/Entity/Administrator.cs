namespace DataLayer.Entity
{
	public sealed class Administrator : Employee
	{
		public int CardId { get; set; }

		public Card Card { get; set; }
	}
}
