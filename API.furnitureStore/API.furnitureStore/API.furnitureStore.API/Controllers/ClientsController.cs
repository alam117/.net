using API.furnitureStore.Data;
using API.furnitureStore.shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.furnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly APIFurnitureStoreContext _context;
        public ClientsController(APIFurnitureStoreContext cont)
        {
            _context = cont;
        }
        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            return await _context.Clients.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getDetails(int id)
        {
            var client=await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (client == null)
            {
                return NotFound();

            }
            return Ok(client);
        }
        [HttpPost]
        public async Task<IActionResult> post(Client client) 
        {
            await _context.Clients.AddAsync(client);
            _context.SaveChanges();
            return CreatedAtAction("Post", client.Id, client);
        }
        [HttpPut]
        public async Task<IActionResult> put(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> delete(Client client)
        {
            if (client== null)
            {
                return NotFound();
            }
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
