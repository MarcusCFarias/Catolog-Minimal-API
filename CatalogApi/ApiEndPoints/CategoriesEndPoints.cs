using CatalogApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebAPI.Context;

namespace CatalogApi.ApiEndPoints
{
    public static class CategoriesEndPoints
    {
        public static void MapCategoriesEndPoints(this WebApplication app)
        {
            app.MapGet("/categories", async (AppDbContext db) =>
            {
                var categories = await db.Categories.ToListAsync();

                if (categories is List<Category>)
                    return Results.Ok(categories);
                else
                    return Results.NotFound();
            }).WithTags("Categories").RequireAuthorization();

            app.MapGet("/categories/{id}", async (AppDbContext db, int id) =>
            {
                var category = await db.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category is Category)
                    return Results.Ok(category);

                return Results.NotFound();
            }).WithTags("Categories").RequireAuthorization();

            app.MapPost("/categories", async (AppDbContext db, Category category) =>
            {
                if (category is null)
                    return Results.NoContent();

                db.Categories.Add(category);
                await db.SaveChangesAsync();

                return Results.Created("/categories/{category.CategoryId}", category);
            }).WithTags("Categories").RequireAuthorization();

            app.MapPut("/categories/{id:int}", async (AppDbContext db, int id, Category category) =>
            {
                if (category.Id != id)
                    return Results.BadRequest();

                var categoryDb = await db.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (categoryDb is null)
                    return Results.NotFound();

                //db.Entry(category).State = EntityState.Modified;
                categoryDb.Name = category.Name;
                categoryDb.Description = category.Description;

                await db.SaveChangesAsync();

                return Results.Ok();
            }).WithTags("Categories").RequireAuthorization();

            app.MapDelete("/categories/{id:int}", async (AppDbContext db, int id) =>
            {
                var categoryDb = await db.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (categoryDb is null)
                    return Results.NotFound();

                db.Categories.Remove(categoryDb);
                await db.SaveChangesAsync();

                return Results.NoContent();
            }).WithTags("Categories").RequireAuthorization();
        }
    }
}
