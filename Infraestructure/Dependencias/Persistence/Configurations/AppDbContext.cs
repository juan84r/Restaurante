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
            // Category
            modelBuilder.Entity<Category>()
                .ToTable("Category") //Nombro la tabla
                .HasKey(c => c.Id); //PK Category.Id

            // DeliveryType
            modelBuilder.Entity<Delivery>()
                .ToTable("DeliveryType")
                .HasKey(dt => dt.Id); //PK Delivery.Id

            // Status
            modelBuilder.Entity<Status>()
                .ToTable("Status")
                .HasKey(s => s.Id);  //PK Status.Id

            // Dish
            modelBuilder.Entity<Dish>()
                .ToTable("Dish")
                .HasKey(d => d.DishId);  //PK Dish.DishId (uuid)

            modelBuilder.Entity<Dish>()
                .HasOne(d => d.Category) //Cada Dish pertenece a una Category
                .WithMany(c => c.Dishes) //Una Category puede tener muchos Dish
                .HasForeignKey(d => d.CategoryId); //FK Dish.Category -> Category.Id

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