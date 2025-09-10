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
    public interface IStatusServices
    {

        Task <Status> CreateStatus(CreateStatusRequest request);

        Task <Status> DeleteStatus(int statusId);

        Task <List<Status>> GetAll();

        Task <Status> GetById(int statusId);
        
    }
}