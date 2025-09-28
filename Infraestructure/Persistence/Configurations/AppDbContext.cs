using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
       public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        // --- Category ---
            modelBuilder.Entity<Category>()
                .ToTable("Category")
                .HasKey(c => c.Id);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Entradas", Description = "Pequeñas porciones para abrir el apetito antes del plato principal.", Order = 1 },
                new Category { Id = 2, Name = "Ensaladas", Description = "Opciones frescas y livianas, ideales como acompañamiento o plato principal.", Order = 2 },
                new Category { Id = 3, Name = "Minutas", Description = "Platos rápidos y clásicos de bodegón: milanesas, tortillas, revueltos.", Order = 3 },
                new Category { Id = 4, Name = "Parrilla", Description = "Cortes de carne asados a la parrilla, servidos con guarniciones.", Order = 4 },
                new Category { Id = 5, Name = "Pastas", Description = "Variedad de pastas caseras y salsas tradicionales.", Order = 5 },
                new Category { Id = 6, Name = "Sandwiches", Description = "Sandwiches y lomitos completos preparados al momento.", Order = 6 },
                new Category { Id = 7, Name = "Pizzas", Description = "Pizzas artesanales con masa casera y variedad de ingredientes.", Order = 7 },
                new Category { Id = 8, Name = "Bebidas", Description = "Gaseosas, jugos, aguas y opciones sin alcohol.", Order = 8 },
                new Category { Id = 9, Name = "Cerveza Artesanal", Description = "Cervezas de producción artesanal, rubias, rojas y negras.", Order = 9 },
                new Category { Id = 10, Name = "Postres", Description = "Clásicos dulces caseros para cerrar la comida.", Order = 10 }
            );

            // --- DeliveryType ---
            modelBuilder.Entity<Delivery>()
                .ToTable("DeliveryType")
                .HasKey(dt => dt.Id);

            modelBuilder.Entity<Delivery>().HasData(
                new Delivery { Id = 1, Name = "Delivery" },
                new Delivery { Id = 2, Name = "Take away" },
                new Delivery { Id = 3, Name = "Dine in" }
            );

            // --- Status ---
            modelBuilder.Entity<Status>()
                .ToTable("Status")
                .HasKey(s => s.Id);

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Pending" },
                new Status { Id = 2, Name = "In progress" },
                new Status { Id = 3, Name = "Ready" },
                new Status { Id = 4, Name = "Delivered" },
                new Status { Id = 5, Name = "Closed" }
            );
            // Dish
            modelBuilder.Entity<Dish>()
                .ToTable("Dish")
                .HasKey(d => d.DishId);  //PK Dish.DishId (uuid)

            modelBuilder.Entity<Dish>()
                .HasOne(d => d.Category) //Cada Dish pertenece a una Category
                .WithMany(c => c.Dishes) //Una Category puede tener muchos Dish
                .HasForeignKey(d => d.CategoryId); //FK Dish.Category -> Category.Id

            modelBuilder.Entity<Dish>()
                .HasIndex(d => d.Name) //Me asuguro de que el nombre sea unico a nivel DB
                .IsUnique();  

            // Order
            modelBuilder.Entity<Order>()
                .ToTable("Order")
                .HasKey(o => o.OrderId); //PK Order.OrderId

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Delivery) //Cada Order tiene un Delivery
                .WithMany(dt => dt.Orders) //Un Delivery puede estar en MUCHOS Orders
                .HasForeignKey(o => o.DeliveryId); //FK: Order.DeliveryId -> Delivery.Id

            modelBuilder.Entity<Order>()
                .HasOne(o => o.OverallStatus) //Cada Order tiene un Status general
                .WithMany(s => s.Orders)  //Un Status puede estar en MUCHOS Orders
                .HasForeignKey(o => o.OverallStatusId); //FK: Order.OverallStatusId -> Status.Id

            // OrderItem
            modelBuilder.Entity<OrderItem>()
                .ToTable("OrderItem")
                .HasKey(oi => oi.OrderItemId); //PK: OrderItem.OrderItemId

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order) //Cada OrderItem pertenece a UN Order
                .WithMany(o => o.OrderItems) //Un Order tiene MUCHOS OrderItems
                .HasForeignKey(oi => oi.OrderId); //FK: OrderItem.OrderId -> Order.OrderId

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Dish) //Cada OrderItem corresponde a UN Dish 
                .WithMany(d => d.OrderItems) //Un Dish puede estar en MUCHOS OrderItems
                .HasForeignKey(oi => oi.DishId); //FK: OrderItem.DishId -> Dish.DishId

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Status) //Cada OrderItem tiene un Status
                .WithMany(s => s.OrderItems) //Un Status puede aplicarse a MUCHOS OrderItems
                .HasForeignKey(oi => oi.StatusId); //FK: OrderItem.StatusId -> Status.Id

        }
    }
}