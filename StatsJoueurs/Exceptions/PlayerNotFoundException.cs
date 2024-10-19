namespace StatsJoueurs.Exceptions
{
    public class PlayerNotFoundException: Exception
    {
        public PlayerNotFoundException(int id)
        : base($"Player with ID {id} not found.") { }
    }
}
