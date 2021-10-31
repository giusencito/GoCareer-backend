using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gocareer.Domain;
using Gocareer.Infrastructure;
using Gocareer_backend.Models.Work;

namespace Gocareer_backend.Controllers
{
    [Route("api")]
    [ApiController]
    public class WorksController : ControllerBase
    {
        private readonly DbContextGocareer _context;

        public WorksController(DbContextGocareer context)
        {
            _context = context;
        }

        // GET: api/Works
        [HttpGet("Works")]
        public async Task<IEnumerable<WorkModel>> GetWorks()
        {
            var workList = await _context.Works.ToListAsync();

            return workList.Select(m => new WorkModel
            {
                Workid = m.Workid,
                WorkName = m.WorkName,
                WorkDescription = m.WorkDescription,
                Careerid = m.Careerid
            });
        }

        // GET: api/Works/5
        [HttpGet("Works/{id}")]
        public async Task<IActionResult> GetWorkById(int id)
        {
            var work = await _context.Works.FindAsync(id);

            if (work == null)
                return NotFound();

            return Ok(new WorkModel
            {
                Workid = work.Workid,
                WorkName = work.WorkName,
                WorkDescription = work.WorkDescription,
                Careerid = work.Careerid
            });
        }

        // GET: api/Options/5
        [HttpGet("Career/{CareerId}/Work")]
        public async Task<ActionResult<Work>> GetWorkByCareerId(int CareerId)
        {
            IEnumerable<Work> workList = await _context.Works.ToListAsync();

            var WorkListByCareerId = workList.ToList().Where(d => d.Careerid == CareerId);

            if (WorkListByCareerId.Count() > 0)
            {
                return Ok(WorkListByCareerId.Select(d => new WorkModel
                {
                    Workid = d.Workid,
                    WorkName = d.WorkName,
                    WorkDescription = d.WorkDescription,
                    Careerid = d.Careerid

                }));
            }
            else
            {
                return Ok("No hay work(s) para el Career.");
            }
        }

        // PUT: api/Works/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutWork(int id, Work work)
        //{
        //    if (id != work.Workid)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(work).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WorkExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Works
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Works")]
        public async Task<IActionResult> PostWork([FromBody] CreateWorkModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Work work = new Work
            {
                WorkName = model.WorkName,
                WorkDescription = model.WorkDescription,
                Careerid = model.Careerid
            };
            _context.Works.Add(work);
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

        // DELETE: api/Works/5
        [HttpDelete("Works/{id}")]
        public async Task<IActionResult> DeleteWork(int id)
        {
            var existingWork = await _context.Works.FindAsync(id);
            if (existingWork == null)
                return NotFound();

            try
            {
                _context.Remove(existingWork);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(existingWork);
        }

        private bool WorkExists(int id)
        {
            return _context.Works.Any(e => e.Workid == id);
        }
    }
}
