using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infraestructure.Persistence;
using Aplication.Interfaces;

namespace Infraestructure.Command
{
    public class DeliveryCommand : IDeliveryCommand
    {
        private readonly AppDbContext _context;

        public DeliveryCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertDelivery(Delivery delivery)
        {
            _context.Add(delivery);

            await _context.SaveChangesAsync();
        }

        public Task RemoveDelivery(int deliveryId)
        {
            throw new NotImplementedException();
        }

    }
}