using Microsoft.EntityFrameworkCore;
using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Infrastructure.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoHotel.Infrastructure.Repositories
{
    public class BaseRepository<TModel> where TModel : BaseEntity
    {
        public SqlDbContext _context;
        public BaseRepository(SqlDbContext context) => _context = context;

        public async Task<ICollection<TModel>> ListarTudo()
            => await _context.Set<TModel>().AsNoTracking().ToListAsync();

        public async Task<TModel> Cadastrar(TModel model)
        {
            await _context.Set<TModel>().AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<TModel> BuscarPorId(long id)
            => await _context.Set<TModel>().FindAsync(id);

        public async Task<TModel> Editar(TModel updated, long id)
        {
            if (updated == null)
                return null;

            TModel obj = await BuscarPorId(id);
            if (obj != null)
            {
                _context.Entry(obj).CurrentValues.SetValues(updated);
                await _context.SaveChangesAsync();
            }
            return obj;
        }

        public async Task Deletar(long id)
        {
            TModel obj = await BuscarPorId(id);
            await Deletar(obj);
        }

        public async Task Deletar(TModel obj)
        {
            if (obj != null)
            {
                _context.Set<TModel>().Remove(obj);
                await _context.SaveChangesAsync();
            }
        }
    }
}
