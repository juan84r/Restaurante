using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Entities;
using Aplication;

namespace Aplication.Services
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
        public async Task<CreateDeliveryResponse> CreateDelivery(CreateDeliveryRequest request)
        {
            var delivery = new Delivery
            {
                Id = request.DeliveryId,
                Name = request.Name,
            };
            await _command.InsertDelivery(delivery);
            return new CreateDeliveryResponse
            {
                DeliveryId = delivery.Id,
                Name = delivery.Name,
            };
        }

        public Task<Delivery> DeleteDelivery(int deliveryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Delivery>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CreateDeliveryResponse> GetById(int deliveryId)
        {
            var delivery = _query.GetDelivery(deliveryId);
            return Task.FromResult(new CreateDeliveryResponse
            {
                DeliveryId = delivery.Id,
                Name = delivery.Name,
            });
        }
    }
}