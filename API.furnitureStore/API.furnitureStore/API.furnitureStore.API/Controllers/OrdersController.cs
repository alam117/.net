using API.furnitureStore.Data;
using API.furnitureStore.shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.furnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly APIFurnitureStoreContext _contex;
        public OrdersController(APIFurnitureStoreContext context)
        {
            _contex = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Order>> get()
        {
            return await _contex.Orders.Include(o=>o.orderDetails).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getDetails(int id)
        {
            var order=await _contex.Orders.Include(o=> o.orderDetails).FirstOrDefaultAsync(o=>o.Id==id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Order order)
        {
            if (order.orderDetails==null)
            {
                return BadRequest("order should be have at least one details");
            }

            await _contex.Orders.AddAsync(order);
            await _contex.SaveChangesAsync();
            var ordenes = order.orderDetails;
            var id = order.Id;
            int total = order.orderDetails.Count;
            if (total>0)
            {
                var counti = 0;
                for (global::System.Int32 i = 0; i < total; i++)
                {
                    ordenes[i].Idorder = id;
                }
            }
            
            await _contex.OrderDetails.AddRangeAsync(ordenes);

            await _contex.SaveChangesAsync();

            return CreatedAtAction("Post", order.Id, order);
        }
        [HttpPut]
        public async Task<IActionResult> put(Order order)
        {
            if (order==null)
            {
                return NotFound();
            }
            if (order.Id <= 0)
            {
                return NotFound();
            }
            var existingOrder= await _contex.Orders.Include(o=>o.orderDetails).FirstOrDefaultAsync(o=> o.Id==order.Id);
            if (existingOrder==null)
            {
                return NotFound();
            }
            existingOrder.OrderNumber= order.OrderNumber;
            existingOrder.OrderDate= order.OrderDate;
            existingOrder.DeliveryDate= order.DeliveryDate;  
            existingOrder.ClientId= order.ClientId;

            _contex.OrderDetails.RemoveRange(existingOrder.orderDetails);
            _contex.Orders.Update(existingOrder);
            _contex.OrderDetails.AddRange(order.orderDetails);

            await _contex.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> delete(Order order)
        {
            if (order == null) 
            {
                return NotFound();
            }
            var existingOrder= await _contex.Orders.Include(o=>o.orderDetails).FirstOrDefaultAsync(o=>o.Id==order.Id);
            if(existingOrder==null) return NotFound();
            _contex.OrderDetails.RemoveRange( existingOrder.orderDetails);
            _contex.Orders.Remove(existingOrder);
            await _contex.SaveChangesAsync();
            return NoContent();

        }
    }
}
