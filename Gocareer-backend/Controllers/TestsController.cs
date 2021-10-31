using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gocareer.Domain;
using Gocareer.Infrastructure;
using Gocareer_backend.Models.Test;

namespace Gocareer_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly DbContextGocareer _context;

        public TestsController(DbContextGocareer context)
        {
            _context = context;
        }

        // GET: api/Tests
        [HttpGet]
        public async Task<IEnumerable<TestModel>> GetTests()
        {
            var testList = await _context.Tests.ToListAsync();

            return testList.Select(m => new TestModel
            {
                Testid = m.Testid,
                Personalized = m.Personalized,
                EspecialistId = m.EspecialistId,
                Testname = m.Testname
            });
        }

        // GET: api/Tests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestById(int id)
        {
            var test = await _context.Tests.FindAsync(id);

            if (test == null)
                return NotFound();

            return Ok(new TestModel
            {
                Testid = test.Testid,
                Personalized = test.Personalized,
                EspecialistId = test.EspecialistId,
                Testname = test.Testname
            });
        }

        // PUT: api/Tests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTest(int id, [FromBody] UpdateTestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest();

            var test = await _context.Tests.FirstOrDefaultAsync(d => d.Testid == id);

            if (test == null)
                return NotFound();

            test.Personalized = model.Personalized;

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

        // POST: api/Tests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostTest([FromBody] CreateTestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Test test = new Test
            {
                Personalized = model.Personalized,
                EspecialistId = model.EspecialistId,
                Testname = model.Testname
            };
            _context.Tests.Add(test);
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

        // DELETE: api/Tests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            var existingTest = await _context.Tests.FindAsync(id);
            if (existingTest == null)
                return NotFound();

            try
            {
                _context.Remove(existingTest);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(existingTest);
        }

        private bool TestExists(int id)
        {
            return _context.Tests.Any(e => e.Testid == id);
        }
    }
}
