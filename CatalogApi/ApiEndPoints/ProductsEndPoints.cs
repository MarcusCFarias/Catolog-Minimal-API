using CatalogApi.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;

namespace CatalogApi.ApiEndPoints
{
    public static class ProductsEndPoints
    {
        public static void MapProductsEndPoints(this WebApplication app)
        {
            app.MapGet("/products", async (AppDbContext db) =>
            {
                var products = await db.Products.ToListAsync();

                if (products is List<Product>)
                    return Results.Ok(products);
                else
                    return Results.NotFound();
            }).WithTags("Products").RequireAuthorization();

            app.MapGet("/products/{id}", async (AppDbContext db, int id) =>
            {
                var product = await db.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (product is Category)
                    return Results.Ok(product);

                return Results.NotFound();
            }).WithTags("Products").RequireAuthorization();

            app.MapPost("/products", async (AppDbContext db, Product product) =>
            {
                if (product is null)
                    return Results.NoContent();

                db.Products.Add(product);
                await db.SaveChangesAsync();

                return Results.Created("/products/{product.Id}", product);
            }).WithTags("Products").RequireAuthorization();

            app.MapPut("/products/{id:int}", async (AppDbContext db, int id, Product product) =>
            {
                if (product.Id != id)
                    return Results.BadRequest();

                var productDb = await db.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (productDb is null)
                    return Results.NotFound();

                productDb.Name = product.Name;
                productDb.Description = product.Description;
                productDb.Price = product.Price;
                productDb.ImgUrl = product.ImgUrl;
                productDb.Stock = product.Stock;
                productDb.PurchaseDate = product.PurchaseDate;
                productDb.CategoryId = product.CategoryId;

                await db.SaveChangesAsync();

                return Results.Ok();
            }).WithTags("Products").RequireAuthorization();

            app.MapDelete("/products/{id:int}", async (AppDbContext db, int id) =>
            {
                var productDb = await db.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (productDb is null)
                    return Results.NotFound();

                db.Products.Remove(productDb);
                await db.SaveChangesAsync();

                return Results.NoContent();
            }).WithTags("Products").RequireAuthorization();
        }
    }
}
