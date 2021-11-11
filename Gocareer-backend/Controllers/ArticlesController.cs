using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gocareer.Domain;
using Gocareer.Infrastructure;
using Gocareer_backend.Models.Article;
namespace Gocareer_backend.Controllers
{
    [Route("api")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly DbContextGocareer _context;

        public ArticlesController(DbContextGocareer context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet("Articles")]
        public async Task<IEnumerable<ArticleModel>> GetArticles()
        {
            var articleList = await _context.Articles.ToListAsync();

            return articleList.Select(m => new ArticleModel
            {
                Articleid = m.Articleid,
                ArticleName = m.ArticleName,
                ArticleDescription = m.ArticleDescription,
                Careerid = m.Careerid
            });
        }


        // GET: api/Articles/5
        [HttpGet("Career/{careerId}/Articles")]
        public async Task<ActionResult<Article>> GetArticlesBySectionId(int careerId)
        {
            IEnumerable<Article> assignmentList = await _context.Articles.ToListAsync();

            var assignmentListBySectionId = assignmentList.ToList().Where(d => d.Careerid == careerId);

            if (assignmentListBySectionId.Count() > 0)
            {
                return Ok(assignmentListBySectionId.Select(d => new ArticleModel
                {
                    Articleid = d.Articleid,
                    ArticleName = d.ArticleName,
                    ArticleDescription = d.ArticleDescription,
                    Careerid = d.Careerid,
                   

                }));
            }
            else
            {
                return Ok("No hay articulos.");
            }
        }


        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("Articles/{id}")]
        //public async Task<IActionResult> PutArticle(int id, [FromBody] UpdateArticleModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (id <= 0)
        //        return BadRequest();

        //    var variable = await _context.Messages.FirstOrDefaultAsync(d => d.Messageid == id);

        //    if (variable == null)
        //        return NotFound();

        //    variable.answer = model.answer;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //    return Ok(model);
        //}

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Career/{CareerId}/Articles")]
        public async Task<ActionResult<Article>> PostAssignment(int CareerId, [FromBody] CreateArticleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Career career = await _context.Careers.FindAsync(CareerId);

            if (career == null)
                return NotFound();

            Article article = new Article
            {
                ArticleName = model.ArticleName,
                ArticleDescription = model.ArticleDescription,
                Careerid = model.Careerid,

                
            };
            _context.Articles.Add(article);
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



        // DELETE: api/Articles/5
        [HttpDelete("Articles/{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var existingAssignment = await _context.Articles.FindAsync(id);
            if (existingAssignment == null)
                return NotFound();

            try
            {
                _context.Remove(existingAssignment);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(existingAssignment);
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Articleid == id);
        }
    }
}
