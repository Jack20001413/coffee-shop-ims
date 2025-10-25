using System;
using CoffeeShopIMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopIMS.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IEnumerable<Ingredient> seedingIngredients =
        [
            new() { Name = "Espresso Roast", Unit = "kg", Quantity = 50, Sku = "ER-001"},
            new() { Name = "Whole Milk", Unit = "liters", Quantity = 100, Sku = "WM-001"},
            new() { Name = "Sugar", Unit = "kg", Quantity = 200, Sku = "SG-001"},
            new() { Name = "Vanilla Syrup", Unit = "liters", Quantity = 50, Sku = "VS-001"}
        ];
        IEnumerable<Warehouse> seedingWarehouses =
        [
            new() { Address = "2637 Jackson Boulevard, CA", PersonInCharge = "Emmy"},
            new() { Address = "347 Midtown, NY", PersonInCharge = "Teddy"}
        ];

        optionsBuilder.UseSeeding((context, _) =>
        {
            bool hasIngredients = Ingredients.Any();
            bool hasWarehouses = Warehouses.Any();

            if ((hasIngredients is false) || (hasWarehouses is false))
            {
                Ingredients.AddRange(seedingIngredients);
                Warehouses.AddRange(seedingWarehouses);
                context.SaveChanges();
            }
        })
        .UseAsyncSeeding(async (context, _, cancellationToken) =>
        {
            bool hasIngredients = await Ingredients.AnyAsync(cancellationToken);
            bool hasWarehouses = await Warehouses.AnyAsync(cancellationToken);

            if ((hasIngredients is false) || (hasWarehouses is false))
            {
                await Ingredients.AddRangeAsync(seedingIngredients, cancellationToken);
                await Warehouses.AddRangeAsync(seedingWarehouses, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
        });
    }
}
