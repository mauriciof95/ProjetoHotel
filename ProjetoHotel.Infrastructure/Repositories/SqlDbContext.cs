using Microsoft.EntityFrameworkCore;
using ProjetoHotel.Domain.Entities;

namespace ProjetoHotel.Infrastructure.Repositories
{
    public class SqlDbContext : DbContext
    {
        public DbSet<Hotel> Hotel { get; set; }

        public DbSet<Quarto> Quarto { get; set; }
    }
}
