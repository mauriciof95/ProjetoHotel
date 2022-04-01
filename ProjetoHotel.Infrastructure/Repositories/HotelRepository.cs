using Microsoft.EntityFrameworkCore;
using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Domain.Models;
using ProjetoHotel.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Hotel> RetornarCompletoPorId(long id)
        {
            return await _context.Hotel
                                .Include(x => x.Imagens)
                                .Include(x => x.Quartos).ThenInclude(x => x.Imagens)
                                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeletarTudo(long id)
        {
            var obj = await RetornarCompletoPorId(id);

            if (obj == null) return;

            _context.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SelectModel>> RetornarHoteisSelect()
        {
            return await _context.Hotel.AsNoTracking()
                                 .Select(x => new SelectModel
                                 {
                                     Id = x.Id,
                                     Descricao = x.Nome
                                 }).ToListAsync();
        }
    }
}
