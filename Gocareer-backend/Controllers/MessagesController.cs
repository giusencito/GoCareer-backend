using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gocareer.Domain;
using Gocareer.Infrastructure;
using Gocareer_backend.Models.Message;

namespace Gocareer_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly DbContextGocareer _context;

        public MessagesController(DbContextGocareer context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<IEnumerable<MessageModel>> GetMessages()
        {
            var messageList = await _context.Messages.ToListAsync();

            return messageList.Select(m => new MessageModel
            {
                Messageid = m.Messageid,
                MessageDescription = m.MessageDescription,
                answer = m.answer,
                UserId = m.UserId,
                EspecialistId = m.EspecialistId
            });
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageById(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
                return NotFound();

            return Ok(new MessageModel
            {
                Messageid = message.Messageid,
                MessageDescription = message.MessageDescription,
                answer = message.answer,
                UserId = message.UserId,
                EspecialistId = message.EspecialistId
            });
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMessage(int id, Message message)
        //{
        //    if (id != message.Messageid)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(message).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MessageExists(id))
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

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] CreateMessageModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Message message = new Message
            {
                MessageDescription = model.MessageDescription,
                answer = model.answer,
                UserId = model.UserId,
                EspecialistId = model.EspecialistId
            };
            _context.Messages.Add(message);
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

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var existingMessage = await _context.Messages.FindAsync(id);
            if (existingMessage == null)
                return NotFound();

            try
            {
                _context.Remove(existingMessage);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(existingMessage);
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Messageid == id);
        }
    }
}
