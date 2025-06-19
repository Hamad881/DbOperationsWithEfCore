using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyHub.Data;
using StudyHub.Entities;

namespace StudyHub.Services
{
    public class CatService : ICatService

    {
        private readonly MyDbContext context;

        public CatService(MyDbContext context)
        {
            this.context = context;
        }

        public async Task<Categories?> AddCategoryAsync(Categories request)
        {

            var category = new Categories();
            if (await context.Categories.AnyAsync(c => c.Cat_Name == request.Cat_Name))
            {
                return null;
            }
            category.Cat_Name = request.Cat_Name;

            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return category;
        }
        public async Task<List<Categories>> GetCategoriesAsync()
        {
            var result = await context.Categories.ToListAsync();
            return result;
        }
        public async Task<ActionResult<Categories>> UpdateCategoryAsync(int id, Categories updatedCategory)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null)
            {
                return null;
            }
            category.Cat_Name = updatedCategory.Cat_Name;
            await context.SaveChangesAsync();
            return category;
        }
    }
}
