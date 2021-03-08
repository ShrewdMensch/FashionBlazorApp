using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppUserRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Owner>();


            builder.Entity<Order>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Id).ValueGeneratedNever();
            });
            
            builder.Entity<Employee>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Id).ValueGeneratedNever();
            });
            
            /*builder.Entity<ReadyToWear>(b =>
            {
                b.HasKey(p => p.Id);
                b.HasMany(p => p.Photos)
                .WithOne().HasForeignKey(p=>p.);
            });*/
        }

        public DbSet<Customer> Address { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Accessory> Accessories { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<DailyRate> DailyRates { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeDesignation> EmployeeDesignations { get; set; }

        public DbSet<IncurredExpense> IncurredExpenses { get; set; }

        public DbSet<Measurement> Measurements { get; set; }

        public DbSet<MeasurementHeader> MeasurementHeaders { get; set; }

        public DbSet<OperatingExpense> OperatingExpensePerDays { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<AppUserPhoto> AppUserPhotos { get; set; }

        public DbSet<ReadyToWearPhoto> ReadyToWearPhotos { get; set; }

        public DbSet<ReadyToWear> ReadyToWears { get; set; }

        public DbSet<RegularCart> RegularCarts { get; set; }

        public DbSet<RegularCartItem> RegularCartItems { get; set; }

        public DbSet<RegularOrder> RegularOrders { get; set; }

        public DbSet<RegularOrderItem> RegularOrderItems { get; set; }

        public DbSet<StandardSize> StandardSizes { get; set; }

        public DbSet<TypeOfCloth> TypeOfCloths { get; set; }

        public DbSet<TypeOfClothAccessory> TypeOfClothAccessories { get; set; }

        public DbSet<TypeOfClothMeasurementHeader> TypeOfClothMeasurementHeaders { get; set; }

        public DbSet<TypeOfClothIncurredExpense> TypeOfClothIncurredExpenses { get; set; }
    }
}
