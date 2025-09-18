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
    public class DeliveryQuery : IDeliveryQuery
    {
        private readonly AppDbContext _context;
        public DeliveryQuery(AppDbContext context)
        {
            _context = context;
        }
        public List<Delivery> GetListDelivery()
        {
            throw new NotImplementedException();
        }

        public Delivery GetDelivery(int deliveryId)
        {
            var delivery = _context.Deliveries
                .FirstOrDefault(s => s.Id == deliveryId);

            if (delivery == null)
                throw new Exception($"Delivery con ID {deliveryId} no encontrado.");

            return delivery;
        }
    }
}