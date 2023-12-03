namespace Ejer2API_AlejandroSerafinParedes.Models
{
    public class Game
    {
        public int gameId { get; set; }
        public string gameName { get; set; }
        public Genre? Genre { get; set; }
        public int genreId { get; set; }
    }
}
