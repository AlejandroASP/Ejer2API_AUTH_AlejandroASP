using System.ComponentModel.DataAnnotations;

namespace Ejer2API_AlejandroSerafinParedes.Models
{
    public class Genre
    {
        [Key]
        public int genreId {  get; set; }
        public string genreName {  get; set; }
        public List<Game> games { get; set; }
    }
}
