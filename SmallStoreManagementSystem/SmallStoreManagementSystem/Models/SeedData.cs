using Microsoft.EntityFrameworkCore;
using SmallStoreManagementSystem.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new SmallStoreManagementSystemContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<SmallStoreManagementSystemContext>>()))
        {
            // Look for any movies.
            if (context.Product.Any())
            {
                return;   // DB has been seeded
            }
            context.Product.AddRange(
                new Product
                {
                    Name = "Laptop 2000",
                    Type = "Electronics",
                    Subtype = "Computers",
                    Price = 1100.99M
                },
                new Product
                {
                    Name = "Desktop computer",
                    Type = "Electronics",
                    Subtype = "Computers",
                    Price = 1000.99M
                },
                new Product
                {
                    Name = "RingRing 1000",
                    Type = "Electronics",
                    Subtype = "Mobile phones",
                    Price = 500M
                },
                new Product
                {
                    Name = "Watch 100",
                    Type = "Electronics",
                    Subtype = "Smart watches",
                    Price = 100.50M
                },
                new Product
                {
                    Name = "Milk",
                    Type = "Food",
                    Subtype = "Dairy products",
                    Price = 2.99M
                },
                new Product
                {
                    Name = "Cheese",
                    Type = "Food",
                    Subtype = "Dairy products",
                    Price = 3.99M
                }, 
                new Product
                {
                    Name = "Chips",
                    Type = "Food",
                    Subtype = "Snacks",
                    Price = 1.99M
                },
                new Product
                {
                    Name = "Sofa",
                    Type = "Furniture",
                    Subtype = "House furniture",
                    Price = 200.15M
                },
                 new Product
                 {
                     Name = "Desk",
                     Type = "Furniture",
                     Subtype = "House furniture",
                     Price = 80.80M
                 },
                 new Product
                 {
                     Name = "Carpet",
                     Type = "Furniture",
                     Subtype = "House furniture",
                     Price = 10.50M
                 }

            );
            context.SaveChanges();
        }
    }
}