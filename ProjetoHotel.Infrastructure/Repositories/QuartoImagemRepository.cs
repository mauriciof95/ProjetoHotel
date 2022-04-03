using Microsoft.EntityFrameworkCore;
using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoHotel.Infrastructure.Repositories
{
    public class QuartoImagemRepository : BaseRepository<QuartoImagem>
    {
        public QuartoImagemRepository(SqlDbContext context) : base(context) { }

        public async Task<List<QuartoImagem>> RetornarPorQuartoId(long id)
            => await _context.QuartoImagem.Where(x => x.Quarto_Id == id).ToListAsync();

        public async Task<QuartoImagem> BuscarPorIdQuartoId(long id, long quarto_id)
            => await _context.QuartoImagem.FirstOrDefaultAsync(x => x.Id == id && x.Quarto_Id == quarto_id);
    }
}
