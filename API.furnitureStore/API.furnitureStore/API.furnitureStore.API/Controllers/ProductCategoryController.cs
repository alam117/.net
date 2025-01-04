using API.furnitureStore.Data;
using API.furnitureStore.shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.furnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly APIFurnitureStoreContext _context;
        public ProductCategoryController(APIFurnitureStoreContext context )
        {
            _context= context;
        }
        [HttpGet]
        public async Task<IEnumerable<productCategory>> Getproduccategories()
        {
            return await _context.ProductCategories.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getdetails(int id)
        {
            var productCategory= await _context.ProductCategories.FirstOrDefaultAsync(x=> x.Id==id);
            if (productCategory == null) {
                return NotFound();
            }
            return Ok(productCategory);
        }

        [HttpPost]
        public async Task<IActionResult> post(productCategory productCategory)
        {
            await _context.ProductCategories.AddAsync(productCategory);
            _context.SaveChanges();
            return CreatedAtAction("Post", productCategory.Id, productCategory);
        }
        [HttpPut]
        public async Task<IActionResult> put(productCategory productCategory)
        {
           _context.Update(productCategory);
            await _context.SaveChangesAsync();
            return Ok(productCategory);
        }
        [HttpDelete]
        public async Task<IActionResult> delete(productCategory productCategory)
        {
            if (productCategory == null)
            {
                return NotFound();
            }
            _context.Remove(productCategory);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
