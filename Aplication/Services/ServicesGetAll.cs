using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;

namespace Aplication.Services
{
    public class ServicesGetAll : IServicesGetAll
    {
        public object GetAll()
        {
            return new { name = "string" };
        }   
    }
}