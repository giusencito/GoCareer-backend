using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gocareer.Domain;
using Gocareer.Infrastructure;
using Gocareer_backend.Models.Especialist;
namespace Gocareer_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialistsController : ControllerBase
    {
        private readonly DbContextGocareer _context;

        public EspecialistsController(DbContextGocareer context)
        {
            _context = context;
        }

        // GET: api/Especialists
        [HttpGet]
        public async Task<IEnumerable<EspecialistModel>> GetEspecialist()
        {
            var especialistList = await _context.Especialists.ToListAsync();

            return especialistList.Select(m => new EspecialistModel
            {
                EspecialistId = m.EspecialistId,
                EspecialistName = m.EspecialistName,
                EspecialistLastName = m.EspecialistLastName,
                EspecialistEmail = m.EspecialistEmail,
                EspecialistPassword = m.EspecialistPassword,
                EspecialistInformation = m.EspecialistInformation
            });
        }

        // GET: api/Especialists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Especialist>> GetEspecialistById(int id)
        {
            IEnumerable<Especialist> List = await _context.Especialists.ToListAsync();

            var ListById = List.ToList().Where(d => d.EspecialistId == id);

            if (ListById.Count() > 0)
            {
                return Ok(ListById.Select(d => new EspecialistModel
                {
                    EspecialistId = d.EspecialistId,
                    EspecialistName = d.EspecialistName,
                    EspecialistLastName = d.EspecialistLastName,
                    EspecialistEmail=d.EspecialistEmail,
                    EspecialistPassword=d.EspecialistPassword,
                    EspecialistInformation=d.EspecialistInformation


                }));
            }
            else
            {
                return Ok("No hay especialistas.");
            }
        }

        // PUT: api/Especialists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialist(int id, [FromBody] UptadeEspecialistModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest();

            var variable = await _context.Especialists.FirstOrDefaultAsync(d => d.EspecialistId == id);

            if (variable == null)
                return NotFound();



            variable.EspecialistName = model.EspecialistName;
            variable.EspecialistLastName = model.EspecialistLastName;
            variable.EspecialistEmail = model.EspecialistEmail;
            variable.EspecialistPassword = model.EspecialistPassword;
            variable.EspecialistInformation = model.EspecialistInformation;

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
        // POST: api/Especialists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostEspecialist([FromBody] CreateEspecialistModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Especialist especialist = new Especialist
            {
                EspecialistName = model.EspecialistName,
                EspecialistLastName = model.EspecialistLastName,
                EspecialistEmail = model.EspecialistEmail,
                EspecialistPassword = model.EspecialistPassword,
                EspecialistInformation = model.EspecialistInformation,
            };
            _context.Especialists.Add(especialist);
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

        // DELETE: api/Especialists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Especialists.FindAsync(id);
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

        private bool EspecialistExists(int id)
        {
            return _context.Especialists.Any(e => e.EspecialistId == id);
        }
    }
}
