using Microsoft.EntityFrameworkCore;
using SMARAPDDesafio.Models;

namespace SMARAPDDesafio.Data
{
    public class SmarapdDesafioContext : DbContext
    {
        public SmarapdDesafioContext(DbContextOptions<SmarapdDesafioContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Scheduling> Schedulings { get; set; }
    }
}