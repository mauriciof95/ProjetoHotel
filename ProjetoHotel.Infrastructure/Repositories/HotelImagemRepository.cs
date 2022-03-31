using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Infrastructure.Context;

namespace ProjetoHotel.Infrastructure.Repositories
{
    public class HotelImagemRepository : BaseRepository<HotelImagem>
    {
        public HotelImagemRepository(SqlDbContext context) : base(context) { }
    }
}
