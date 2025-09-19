using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Response;
using Aplication.UseCase.Restaurante.Create.Models;
using Aplication.UseCase.Restaurante.Update.Models;
using Domain.Entities;

namespace Aplication.Interfaces
{
    public interface IDishCommand
{
    Task<CreateDishResponse> CreateDishAsync(CreateDishRequest request);
    Task<CreateDishResponse> UpdateDishAsync(UpdateDishRequest request);
}

}