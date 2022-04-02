using Microsoft.EntityFrameworkCore;
using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHotel.Infrastructure.Repositories
{
    public class HotelImagemRepository : BaseRepository<HotelImagem>
    {
        public HotelImagemRepository(SqlDbContext context) : base(context) { }

        public async Task<List<HotelImagem>> RetornarPorHotelId(long id)
            => await _context.HotelImagem.Where(x => x.Hotel_Id == id).ToListAsync();

        public async Task<HotelImagem> BuscarPorIdHotelId(long id, long hotel_id)
            => await _context.HotelImagem.FirstOrDefaultAsync(x => x.Id == id && x.Hotel_Id == hotel_id);
    }
}
