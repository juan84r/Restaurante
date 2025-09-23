using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infraestructure.Persistence;
using Aplication.Interfaces;

namespace Infraestructure.Commands
{
    public class StatusCommand : IStatusCommand
    {
        private readonly AppDbContext _context;

        public StatusCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertStatus(Status status)
        {
            _context.Add(status);

            await _context.SaveChangesAsync();
        }

        public Task RemoveStatus(int statusId)
        {
            throw new NotImplementedException();
        }

    }
}