using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Aplication.Response
{
    public class CreateDeliveryResponse
    {
        public int DeliveryId { get; set; }
        
        public string Name { get; set; } = string.Empty;
    }
}