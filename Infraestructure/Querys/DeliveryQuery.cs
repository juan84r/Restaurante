using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Entities;

namespace Infraestructure.Querys
{
    public class DeliveryQuery : IDeliveryQuery
    {
        public List<Delivery> GetListDelivery()
        {
            throw new NotImplementedException();
        }

        public Delivery GetDelivery(int deliveryId)
        {
            throw new NotImplementedException();
        }
    }
}