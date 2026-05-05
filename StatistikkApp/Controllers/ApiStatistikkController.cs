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
            .Select(s => new
            {
                s.Id,
                s.Aar,
                s.Verdi,
                Kommune = s.Kommune.Navn,
                Kategori = s.StatistikkKategori.Navn
            })
            .ToListAsync();

        return Ok(data);
    }
    }
}