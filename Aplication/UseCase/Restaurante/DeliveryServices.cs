using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Aplication.Interfaces;
using System.Diagnostics.CodeAnalysis;
using Aplication.UseCase.Restaurante.Create.Models;

namespace Aplication.UseCase.Restaurante
{
    public class DeliveryServices : IDeliveryServices
    {
        private readonly IDeliveryCommand _command;
        private readonly IDeliveryQuery _query;

        public DeliveryServices(IDeliveryCommand command, IDeliveryQuery query)
        {
            _command = command;
            _query = query;
        }
        public async Task<Delivery> CreateDelivery(CreateDeliveryRequest request)
        {
            var delivery = new Delivery
            {
                Id = request.DeliveryId,
                Name = request.Name,
            };
            await _command.InsertDelivery(delivery);
            return delivery;
        }

        public Task<Delivery> DeleteDelivery(int deliveryId)
        {
            throw new NotImplementedException();
        }

        public Task <List<Delivery>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Delivery> GetById(int deliveryId)
        {
            throw new NotImplementedException();
        }
    }
}