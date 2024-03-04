using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnimeGuess.Models;

namespace AnimeGuess.Data
{
    public class AnimeGuessContext : DbContext
    {
        public AnimeGuessContext (DbContextOptions<AnimeGuessContext> options)
            : base(options)
        {
        }

        public DbSet<AnimeGuess.Models.Problem> Problem { get; set; } = default!;
    }
}
