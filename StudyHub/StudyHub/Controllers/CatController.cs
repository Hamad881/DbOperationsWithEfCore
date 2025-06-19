using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.Entities;
using StudyHub.Services;

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase

    {
        private readonly ICatService _catService;
        public CatController(ICatService catService)
        {
            _catService = catService;
        }



        [HttpPost]

        public async Task<ActionResult<Categories>> AddCategory(Categories request)
        {
            var category = await _catService.AddCategoryAsync(request);

            if (category == null)
            {
                return BadRequest("Category Already Exists!");
            }
            return Ok(category);

        }

        [HttpGet]
        public async Task<List<Categories>> GetCategories()
        {
            var result = await _catService.GetCategoriesAsync();
            return result;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Categories>> UpdateCategory(int id,Categories request)
        {
            var updatedCat= await _catService.UpdateCategoryAsync(id, request);
            if (updatedCat == null)
            {
                return BadRequest("Category Not Found");
            }
            return Ok(updatedCat);
        }

    }
}
