using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Dto;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    
    [ApiController]
    [Route("api/Department")]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

         private readonly IMapper _mapper;
        
        public DepartmentController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }

        // GET: api/Department
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentZone>>> GetDepartmentZone()
        {
            var DepartmentZone =await _context.DepartmentZone.ToListAsync();
            return DepartmentZone;
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentZone>> GetDepartmentZone(long id)
        {
            var DepartmentZone = await _context.DepartmentZone.FindAsync(id);

            if (DepartmentZone == null)
            {
                return NotFound();
            }

            return DepartmentZone;
        }

        // POST: api/Department
        [HttpPost]
        public async Task<ActionResult<DepartmentZone>> PostDepartmentZone(NewDepartmentDTO ZoneDTO)
        {
            var zone = _mapper.Map<DepartmentZone>(ZoneDTO);
            _context.DepartmentZone.Add(zone);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartmentZone), new { id = zone.Id }, zone);

        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentZone(long id)
        {
            var DepartmentZone = await _context.DepartmentZone.FindAsync(id);

            if (DepartmentZone == null)
            {
                return NotFound();
            }

            _context.DepartmentZone.Remove(DepartmentZone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}