using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCase.Restaurante.Create.Models
{
    public class CreateDeliveryRequest
    {
        public int DeliveryId { get; set; }

        public string Name { get; set; } = string.Empty;

    }
}