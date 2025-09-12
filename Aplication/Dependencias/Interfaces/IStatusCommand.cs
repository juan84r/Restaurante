using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
namespace Aplication.Interfaces
{
    public interface IStatusCommand
    {
        Task InsertStatus(Status status);

        Task RemoveStatus(int statusId);
        
    }
}