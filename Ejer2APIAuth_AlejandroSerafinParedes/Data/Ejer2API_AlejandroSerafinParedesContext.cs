using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ejer2API_AlejandroSerafinParedes.Models;

namespace Ejer2API_AlejandroSerafinParedes.Data
{
    public class Ejer2API_AlejandroSerafinParedesContext : DbContext
    {
        public Ejer2API_AlejandroSerafinParedesContext (DbContextOptions<Ejer2API_AlejandroSerafinParedesContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    gameId = 1,
                    gameName = "El señor de los ajillos",
                    genreId = 1,
                },
                new Game
                {
                    gameId = 2,
                    gameName = "Estreet Nocilla",
                    genreId = 1,
                },
                new Game
                {
                    gameId = 3,
                    gameName = "Blancanieves y los 7 guanches",
                    genreId = 1,
                }
            );
            modelBuilder.Entity<Genre>().HasData(
                new Genre { genreId=1,genreName="Lucha"},
                new Genre { genreId = 2, genreName = "Acción" },
                new Genre { genreId = 3, genreName = "Shooter" }
                );
        }
        public DbSet<Ejer2API_AlejandroSerafinParedes.Models.Game> Game { get; set; } = default!;

        public DbSet<Ejer2API_AlejandroSerafinParedes.Models.Genre>? Genre { get; set; }
    }
}
