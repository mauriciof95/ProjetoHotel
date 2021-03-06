using Microsoft.EntityFrameworkCore;
using ProjetoHotel.Domain.Entities;

namespace ProjetoHotel.Infrastructure.Context
{
    public class SqlDbContext : DbContext
    {
        public static readonly string ConnectionString = "Server=.;Database=Hotel_DB;User Id=sa;Password=123456;";

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
       
        public SqlDbContext() { }

        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<HotelImagem> HotelImagem { get; set; }
        public DbSet<Quarto> Quarto { get; set; }
        public DbSet<QuartoImagem> QuartoImagem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(ConnectionString);
    }
}
