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
using Gocareer_backend.Models.User_Test;

namespace Gocareer_backend.Controllers
{
    [Route("api")]
    [ApiController]
    public class User_TestController : ControllerBase
    {
        private readonly DbContextGocareer _context;

        Random r = new Random();
        public User_TestController(DbContextGocareer context)
        {
            _context = context;
        }

       //GET: api/User_Test
       [HttpGet("User_Test")]
        public async Task<IEnumerable<User_TestModel>> GetUser_Tests()
        {
            var usertestList = await _context.User_Tests.ToListAsync();

            return usertestList.Select(m => new User_TestModel
            {
                UserId = m.UserId,
                Testid = m.Testid,
                Careerid = m.Careerid,
                releasedate = m.releasedate,
                Points = m.Points
            });
        }

        // GET: api/User_Test/5
        [HttpGet("User/{UserId}/Tests")]
        public async Task<IEnumerable<TestModel>> GetTestsByUserId(int UserId)
        {
            var UserTests = await _context.User_Tests
               .Include(st => st.Estudiante)
               .Include(st => st.Test)
               .Where(x => x.UserId.Equals(UserId))
               .ToListAsync();

            if (UserTests == null)
                return null;

            return UserTests.Select(x => new TestModel
            {
                Testid = x.Testid,
                Personalized = x.Test.Personalized,
                EspecialistId = x.Test.EspecialistId,
                Testname = x.Test.Testname

            });
        }

        [HttpPatch("User/{UserId}/Test/{TestId}/Career/{CareerId}")]
        public async Task<IActionResult> AssignTest(int UserId, int TestId,int CareerId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Estudiante user = await _context.Estudiantes.FindAsync(UserId);
            Test test = await _context.Tests.FindAsync(TestId);
            Career career = await _context.Careers.FindAsync(CareerId);

            if (user == null)
                return NotFound();

            if (test == null)
                return NotFound();

            if (career == null)
                return NotFound();

            User_Test newAssign = new User_Test
            {
                UserId = UserId,
                Testid = TestId,
                releasedate = DateTime.Now,
                Careerid = CareerId,
                Points = r.Next(0, 6)
            };

            await _context.User_Tests.AddAsync(newAssign);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE: api/User_Test/5
        [HttpDelete("User/{UserId}/Test/{TestId}")]
        public async Task<IActionResult> UnAssignTest(int UserId,int TestId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User_Test userTest = await _context.User_Tests
                .Where(x => x.UserId.Equals(UserId))
                .Where(y => y.Testid.Equals(TestId))
                .FirstOrDefaultAsync();

            if (userTest == null)
                return NotFound();

            try
            {
                _context.Remove(userTest);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("User/{Userid}/Test/{Testid}/Career/{Careerid}")]
        public async Task<IActionResult> PutPoints(int Userid,int Testid,int Careerid, [FromBody] UpdateUser_TestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Userid <= 0)
                return BadRequest();

            if (Testid <= 0)
                return BadRequest();

            if (Careerid <= 0)
                return BadRequest();

            var user_test = await _context.User_Tests.FirstOrDefaultAsync(d => d.UserId == Userid && d.Testid == Testid && d.Careerid == Careerid);

            if (user_test == null)
                return NotFound();

            user_test.Points = model.Points;

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

        private bool User_TestExists(int id)
        {
            return _context.User_Tests.Any(e => e.UserId == id);
        }
    }
}
