using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Infrastructure.Context;

namespace ProjetoHotel.Infrastructure.Repositories
{
    public class QuartoImagemRepository : BaseRepository<QuartoImagem>
    {
        public QuartoImagemRepository(SqlDbContext context) : base(context) { }
    }
}
