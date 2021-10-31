using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gocareer.Domain;
using Gocareer.Infrastructure;
using Gocareer_backend.Models.Option;

namespace Gocareer_backend.Controllers
{
    [Route("api")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly DbContextGocareer _context;

        public OptionsController(DbContextGocareer context)
        {
            _context = context;
        }

        // GET: api/Options
        [HttpGet("Options")]
        public async Task<IEnumerable<OptionModel>> GetOptions()
        {
            var optionList = await _context.Options.ToListAsync();

            return optionList.Select(m => new OptionModel
            {
                Optionid = m.Optionid,
                OptionName = m.OptionName,
                Points = m.Points,
                QuestionId = m.QuestionId
            });
        }

        // GET: api/Options/5
        [HttpGet("Options/{id}")]
        public async Task<IActionResult> GetOptionById(int id)
        {
            var option = await _context.Options.FindAsync(id);

            if (option == null)
                return NotFound();

            return Ok(new OptionModel
            {
                Optionid = option.Optionid,
                OptionName = option.OptionName,
                Points = option.Points,
                QuestionId = option.QuestionId
            });
        }

        // GET: api/Options/5
        [HttpGet("Question/{QuestionId}/Option")]
        public async Task<ActionResult<Option>> GetOptionByQuestionId(int QuestionId)
        {
            IEnumerable<Option> optionList = await _context.Options.ToListAsync();

            var OptionListByQuestionId = optionList.ToList().Where(d => d.QuestionId == QuestionId);

            if (OptionListByQuestionId.Count() > 0)
            {
                return Ok(OptionListByQuestionId.Select(d => new OptionModel
                {
                    Optionid = d.Optionid,
                    OptionName = d.OptionName,
                    Points = d.Points,
                    QuestionId = d.QuestionId

                }));
            }
            else
            {
                return Ok("No hay option(s) para el Question.");
            }
        }


        // PUT: api/Options/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Options/{id}")]
        public async Task<IActionResult> PutOption(int id, [FromBody] UpdateOptionModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest();

            var option = await _context.Options.FirstOrDefaultAsync(d => d.Optionid == id);

            if (option == null)
                return NotFound();

            option.Points = model.Points;

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

        // POST: api/Options
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Options")]
        public async Task<IActionResult> PostOption([FromBody] CreateOptionModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Option option = new Option
            {
                OptionName = model.OptionName,
                Points = model.Points,
                QuestionId = model.QuestionId
            };
            _context.Options.Add(option);
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

        // DELETE: api/Options/5
        [HttpDelete("Options/{id}")]
        public async Task<IActionResult> DeleteOption(int id)
        {
            var existingOption = await _context.Options.FindAsync(id);
            if (existingOption == null)
                return NotFound();

            try
            {
                _context.Remove(existingOption);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(existingOption);
        }

        private bool OptionExists(int id)
        {
            return _context.Options.Any(e => e.Optionid == id);
        }
    }
}
