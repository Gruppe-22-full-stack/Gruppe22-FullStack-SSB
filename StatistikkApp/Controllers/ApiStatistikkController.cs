using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatistikkApp.Data;

namespace StatistikkApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiStatistikkController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiStatistikkController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _context.StatistikkData
                .Include(s => s.Kommune)
                .Include(s => s.StatistikkKategori)
                .ToListAsync();

            return Ok(data);
        }
    }
}