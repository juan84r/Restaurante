using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Aplication.UseCase.Restaurante.Create.Models;
using Aplication.Interfaces;
using System.Diagnostics.CodeAnalysis;
using Aplication.Response;

namespace Aplication.Interfaces
{
    public interface IDeliveryServices
    {

        Task <CreateDeliveryResponse> CreateDelivery(CreateDeliveryRequest request);

        Task <Delivery> DeleteDelivery(int deliveryId);

        Task <List<Delivery>> GetAll();

        Task <CreateDeliveryResponse> GetById(int deliveryId);
        
    }
}