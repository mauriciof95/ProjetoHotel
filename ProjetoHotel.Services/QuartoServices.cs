using ProjetoHotel.Domain.Entities;
using ProjetoHotel.Infrastructure.Context;
using ProjetoHotel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoHotel.Services
{
    public class QuartoServices
    {
        private QuartoRepository _repository;
        public QuartoServices(SqlDbContext context)
        {
            _repository = new QuartoRepository(context);
        }
        
    }
}
