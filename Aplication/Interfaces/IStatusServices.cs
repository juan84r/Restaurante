using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Aplication;
using Aplication.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Aplication.Interfaces
{
    public interface IStatusServices
    {

        Task <CreateStatusResponse> CreateStatus(CreateStatusRequest request);

        Task <Status> DeleteStatus(int statusId);

        Task <List<Status>> GetAll();

        Task <CreateStatusResponse> GetById(int statusId);
        
    }
}