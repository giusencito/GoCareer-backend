using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gocareer.Domain;
using Gocareer.Infrastructure;
using Gocareer_backend.Models.Career;
namespace Gocareer_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareersController : ControllerBase
    {
        private readonly DbContextGocareer _context;

        public CareersController(DbContextGocareer context)
        {
            _context = context;
        }

        // GET: api/Careers
        [HttpGet]
        
        public async Task<IEnumerable<CareerModel>> GetCareers()
        {
            var careerList = await _context.Careers.ToListAsync();

            return careerList.Select(m => new CareerModel
            {
                Careerid = m.Careerid,
                CareerName = m.CareerName,
                CareerDescription = m.CareerDescription,
            });
        }
        // GET: api/Careers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Career>> GetCareersBySectionId(int id)
        {
            IEnumerable<Career> assignmentList = await _context.Careers.ToListAsync();

            var assignmentListBySectionId = assignmentList.ToList().Where(d => d.Careerid == id);

            if (assignmentListBySectionId.Count() > 0)
            {
                return Ok(assignmentListBySectionId.Select(d => new CareerModel
                {
                    Careerid = d.Careerid,
                    CareerName = d.CareerName,
                    CareerDescription = d.CareerDescription,
                    


                }));
            }
            else
            {
                return Ok("No hay carreras.");
            }
        }

        // PUT: api/Careers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentType(int id, [FromBody] UpdateCareerModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest();

            var variable = await _context.Careers.FirstOrDefaultAsync(d => d.Careerid == id);

            if (variable == null)
                return NotFound();



            variable.CareerName = model.CareerName;
            variable.CareerDescription = model.CareerDescription;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(model);


        }

        // POST: api/Careers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCareer([FromBody] CreateCareerModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Career career = new Career
            {
                CareerName = model.CareerName,
                CareerDescription=model.CareerDescription
            };
            _context.Careers.Add(career);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(model);

        }

        // DELETE: api/Careers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Careers.FindAsync(id);
            if (existing == null)
                return NotFound();

            try
            {
                _context.Remove(existing);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(existing);
        }

        private bool CareerExists(int id)
        {
            return _context.Careers.Any(e => e.Careerid == id);
        }
    }
}
