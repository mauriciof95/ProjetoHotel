using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Infrastructure.Context;

namespace ProjetoHotel.Infrastructure.Repositories
{
    public class QuartoRepository : BaseRepository<Quarto>
    {
        public QuartoRepository(SqlDbContext context) : base(context) { }
    }
}
