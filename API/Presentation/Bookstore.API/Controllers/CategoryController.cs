using Bookstore.Application.Logics.CategoryLogics;
using Bookstore.Application.Logics.CategoryLogics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Authorize]
    public class CategoryController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryModel model)
        {
            return Ok(await Mediator.Send(new CreateCategory { Model = model }));
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            return Ok(await Mediator.Send(new GetCategories()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int id)
        {
            return Ok(await Mediator.Send(new GetCategoryById() { CategoryId = id }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] CategoryModel model)
        {
            return Ok(await Mediator.Send(new UpdateCategory { Model = model }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DeleteCategory { CategoryId = id }));
        }
    }
}
