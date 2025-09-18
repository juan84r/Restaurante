using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Querys
{
    public class StatusQuery : IStatusQuery
    {
        private readonly AppDbContext _context;
        public StatusQuery(AppDbContext context)
        {
            _context = context;
        }
        public List<Status> GetListStatus()
        {
            throw new NotImplementedException();
        }

        public Status GetStatus(int statusId)
        {
            var status = _context.Statuses
                .FirstOrDefault(s => s.Id == statusId);

            if (status == null)
                throw new Exception($"Status con ID {statusId} no encontrado.");

            return status;
        }
    }
}