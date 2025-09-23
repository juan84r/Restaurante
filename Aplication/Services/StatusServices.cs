using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Entities;
using Aplication;

namespace Aplication.Services
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
        public async Task<CreateStatusResponse> CreateStatus(CreateStatusRequest request)
        {
            var status = new Status
            {
                Id = request.StatusId,
                Name = request.Name,
            };
            await _command.InsertStatus(status);
            return new CreateStatusResponse
            {
                StatusId = status.Id,
                Name = status.Name,
            };
        }

        public Task<Status> DeleteStatus(int statusId)
        {
            throw new NotImplementedException();
        }

        public Task <List<Status>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CreateStatusResponse> GetById(int statusId)
        {
            var status = _query.GetStatus(statusId);
            return Task.FromResult(new CreateStatusResponse
            {
                StatusId = status.Id,
                Name = status.Name,
            });
        }
       
    }
}