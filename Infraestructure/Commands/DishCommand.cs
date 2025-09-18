using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infraestructure.Persistence;
using Aplication.Interfaces;
using Aplication.UseCase.Restaurante.Create.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Command
{
    public class DishCommand : IDishCommand
    {
        private readonly AppDbContext _context;

        public DishCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertDish(Dish dish)
        {
            _context.Add(dish);

            await _context.SaveChangesAsync();
        }

        public Task RemoveDish(int dishId)
        {
            throw new NotImplementedException();
        }

        public async Task <Dish?>UpdateDish(Guid id, CreateDishRequest request)
        {
            // Buscar el plato existente
            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null) return null;

            // Actualizar solo los campos que querés permitir
            dish.Name = request.Name;
            dish.Description = request.Description;
            dish.Price = request.Price;
            dish.Available = request.Available;
            dish.ImageUrl = request.ImageUrl;

            // Si querés actualizar la categoría, primero validar que exista
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == request.CategoryId);
            if (categoryExists)
            {
                dish.CategoryId = request.CategoryId;
            }
            else
            {
                // O lanzás un error o ignorás la actualización de la categoría
                throw new Exception("La categoría indicada no existe");
            }

            dish.UpdateDate = DateTime.UtcNow;

            // Guardar los cambios
            await _context.SaveChangesAsync();

            return dish;
        }
        

        public Task DeleteDish(int dishId)
        {
            throw new NotImplementedException();
        }
    }
}