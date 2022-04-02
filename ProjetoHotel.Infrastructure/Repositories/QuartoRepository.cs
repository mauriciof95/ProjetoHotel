using Microsoft.EntityFrameworkCore;
using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoHotel.Infrastructure.Repositories
{
    public class QuartoRepository : BaseRepository<Quarto>
    {
        public QuartoRepository(SqlDbContext context) : base(context) { }

        public async Task<ICollection<Quarto>> ListarTudo()
        {
            return await _context.Quarto.AsNoTracking()
                                        .Include(x => x.Hotel)
                                        .Include(x => x.Imagens)
                                        .ToListAsync();
        }

        public async Task<Quarto> RetornarCompletoPorId(long id)
        {
            return await _context.Quarto
                                .Include(x => x.Imagens)
                                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeletarTudo(long id)
        {
            var obj = await RetornarCompletoPorId(id);

            if (obj == null) return;

            _context.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> TemQuartosPorHotelId(long id)
            => await _context.Quarto.AnyAsync(x => x.Hotel_Id == id);

    }
}
