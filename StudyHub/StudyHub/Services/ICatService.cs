using Microsoft.AspNetCore.Mvc;
using StudyHub.Entities;

namespace StudyHub.Services
{
    public interface ICatService
    {
        Task<Categories> AddCategoryAsync(Categories request);

        Task<List<Categories>> GetCategoriesAsync();
        Task<ActionResult<Categories>> UpdateCategoryAsync(int id, Categories updatedCategory);
    }
}