using API.furnitureStore.Data;
using API.furnitureStore.shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.furnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly APIFurnitureStoreContext _context;
        public ProductController(APIFurnitureStoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _context.products.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getDetails(int id) 
        {
            var product= await _context.products.FirstOrDefaultAsync(x=>x.Id==id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> post(Product product)
        {
            await _context.products.AddAsync(product);
            _context.SaveChanges();
            return CreatedAtAction("Post", product.Id, product);
        }
        [HttpPut]
        public async Task<IActionResult> put(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        [HttpDelete]
        public async Task<IActionResult> delete(Product product)
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
