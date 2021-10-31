using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gocareer.Domain;
using Gocareer.Infrastructure;
using Gocareer_backend.Models.Estudiantes;
namespace Gocareer_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly DbContextGocareer _context;

        public EstudiantesController(DbContextGocareer context)
        {
            _context = context;
        }

        // GET: api/Estudiantes
        [HttpGet]
        public async Task<IEnumerable<EstudianteModel>> GetEstudiante()
        {
            var estudianteList = await _context.Estudiantes.ToListAsync();

            return estudianteList.Select(m => new EstudianteModel
            {
                UserId = m.UserId,
                UserName = m.UserName,
                UserLastname = m.UserLastname,
                Useremail = m.Useremail,
                UserPassword = m.UserPassword,
            });
        }

        // GET: api/Estudiantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetEstudianteById(int id)
        {
            IEnumerable<Estudiante> List = await _context.Estudiantes.ToListAsync();

            var ListById = List.ToList().Where(d => d.UserId == id);

            if (ListById.Count() > 0)
            {
                return Ok(ListById.Select(d => new EstudianteModel
                {
                    UserId = d.UserId,
                    UserName = d.UserName,
                    UserLastname = d.UserLastname,
                    Useremail = d.Useremail,
                    UserPassword = d.UserPassword,
                    


                }));
            }
            else
            {
                return Ok("No hay estudiantes.");
            }
        }

        // PUT: api/Estudiantes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(int id, [FromBody] UpdateEstudianteModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest();

            var variable = await _context.Estudiantes.FirstOrDefaultAsync(d => d.UserId == id);

            if (variable == null)
                return NotFound();



            variable.UserName = model.UserName;
            variable.UserLastname = model.UserLastname;
            variable.Useremail = model.Useremail;
            variable.UserPassword = model.UserPassword;
            

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
        // POST: api/Estudiantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostEstudiante([FromBody] CreateEstudianteModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Estudiante estudiante = new Estudiante
            {
                UserName = model.UserName,
                UserLastname = model.UserLastname,
                Useremail = model.Useremail,
                UserPassword = model.UserPassword,
                
            };
            _context.Estudiantes.Add(estudiante);
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
        // DELETE: api/Estudiantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Estudiantes.FindAsync(id);
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

        

        private bool EstudianteExists(int id)
        {
            return _context.Estudiantes.Any(e => e.UserId == id);
        }
    }
}
