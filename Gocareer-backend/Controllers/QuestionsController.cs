using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gocareer.Domain;
using Gocareer.Infrastructure;
using Gocareer_backend.Models.Question;

namespace Gocareer_backend.Controllers
{
    [Route("api")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly DbContextGocareer _context;

        public QuestionsController(DbContextGocareer context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet("Questions")]
        public async Task<IEnumerable<QuestionModel>> GetQuestions()
        {
            var questionList = await _context.Questions.ToListAsync();

            return questionList.Select(m => new QuestionModel
            {
                QuestionId = m.QuestionId,
                QuestionName = m.QuestionName,
                Score = m.Score,
                Testid = m.Testid
            });
        }

        // GET: api/Questions/5
        [HttpGet("Questions/{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
                return NotFound();

            return Ok(new QuestionModel
            {
                QuestionId = question.QuestionId,
                QuestionName = question.QuestionName,
                Score = question.Score,
                Testid = question.Testid
            });
        }

        // GET: api/Options/5
        [HttpGet("Test/{TestId}/Question")]
        public async Task<ActionResult<Question>> GetQuestionByTestId(int TestId)
        {
            IEnumerable<Question> questionList = await _context.Questions.ToListAsync();

            var QuestionListByTestId = questionList.ToList().Where(d => d.Testid == TestId);

            if (QuestionListByTestId.Count() > 0)
            {
                return Ok(QuestionListByTestId.Select(d => new QuestionModel
                {
                    QuestionId = d.QuestionId,
                    QuestionName = d.QuestionName,
                    Score = d.Score,
                    Testid = d.Testid

                }));
            }
            else
            {
                return Ok("No hay question(s) para el Test.");
            }
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Questions/{id}")]
        public async Task<IActionResult> PutQuestion(int id, [FromBody] UpdateQuestionModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest();

            var question = await _context.Questions.FirstOrDefaultAsync(d => d.QuestionId == id);

            if (question == null)
                return NotFound();

            question.Score = model.Score;

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

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Questions")]
        public async Task<IActionResult> PostQuestion([FromBody] CreateQuestionModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Question question = new Question
            {
                QuestionName = model.QuestionName,
                Score = model.Score,
                Testid = model.Testid
            };
            _context.Questions.Add(question);
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

        // DELETE: api/Questions/5
        [HttpDelete("Questions/{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var existingQuestion = await _context.Questions.FindAsync(id);
            if (existingQuestion == null)
                return NotFound();

            try
            {
                _context.Remove(existingQuestion);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(existingQuestion);
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }
    }
}
