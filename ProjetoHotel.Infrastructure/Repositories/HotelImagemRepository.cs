using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Infrastructure.Context;
using System;
using System.Threading.Tasks;

namespace ProjetoHotel.Infrastructure.Repositories
{
    public class HotelImagemRepository : BaseRepository<HotelImagem>
    {
        public HotelImagemRepository(SqlDbContext context) : base(context) { }
    }
}
