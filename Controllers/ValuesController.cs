using Microsoft.AspNetCore.Mvc;
using CityGuide.Data;
using Microsoft.EntityFrameworkCore;
using CityGuide.Models;

namespace CityGuide.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetValues()
        {
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Value>> Get(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(v => v.Id == id);

            if (value == null)
            {
                return NotFound();
            }

            return Ok(value);
        }
    }
}