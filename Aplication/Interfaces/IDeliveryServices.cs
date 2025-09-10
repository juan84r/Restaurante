using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Aplication.UseCase.Restaurante.Create.Models;
using Aplication.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Aplication.Interfaces
{
    public interface IDeliveryServices
    {

        Task <Delivery> CreateDelivery(CreateDeliveryRequest request);

        Task <Delivery> DeleteDelivery(int deliveryId);

        Task <List<Delivery>> GetAll();

        Task <Delivery> GetById(int deliveryId);
        
    }
}