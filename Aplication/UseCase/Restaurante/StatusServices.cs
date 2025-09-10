using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Aplication.Interfaces;
using System.Diagnostics.CodeAnalysis;
using Aplication.UseCase.Restaurante.Create.Models;

namespace Aplication.UseCase.Restaurante
{
    public class StatusServices : IStatusServices
    {
        private readonly IStatusCommand _command;
        private readonly IStatusQuery _query;

        public StatusServices(IStatusCommand command, IStatusQuery query)
        {
            _command = command;
            _query = query;
        }
        public async Task<Status> CreateStatus(CreateStatusRequest request)
        {
            var status = new Status
            {
                Id = request.StatusId,
                Name = request.Name,
            };
            await _command.InsertStatus(status);
            return status;
        }

        public Task<Status> DeleteStatus(int statusId)
        {
            throw new NotImplementedException();
        }

        public Task <List<Status>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Status> GetById(int statusId)
        {
            throw new NotImplementedException();
        }
    }
}