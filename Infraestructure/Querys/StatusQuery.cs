using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Entities;

namespace Infraestructure.Querys
{
    public class StatusQuery : IStatusQuery
    {
        public List<Status> GetListStatus()
        {
            throw new NotImplementedException();
        }

        public Status GetStatus(int statusId)
        {
            throw new NotImplementedException();
        }
    }
}