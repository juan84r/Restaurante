using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Aplication.Response
{
    public class CreateStatusResponse
    {
        public int StatusId { get; set; }
        
        public string Name { get; set; } = string.Empty;
    }
}