using Microsoft.EntityFrameworkCore;
using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Infrastructure.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoHotel.Infrastructure.Repositories
{
    public class HotelRepository : BaseRepository<Hotel>
    {
        public HotelRepository(SqlDbContext context) : base(context) { }

        public async Task<ICollection<Hotel>> ListarTudo()
        {
            return await _context.Hotel.AsNoTracking()
                .Include(x => x.Imagens)
                .ToListAsync();
        }
    }
}
