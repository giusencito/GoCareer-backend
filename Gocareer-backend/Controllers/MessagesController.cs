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
    [Route("api")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly DbContextGocareer _context;

        public MessagesController(DbContextGocareer context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet("Messages")]
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
        [HttpGet("Messages/{id}")]
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

        [HttpGet("Especialists/{EspecialistId}/Messages")]
        public async Task<ActionResult<Message>> GetMessagesByEspecialistId(int EspecialistId)
        {
            IEnumerable<Message> messageList = await _context.Messages.ToListAsync();

            var MessageListByEspecialistId = messageList.ToList().Where(d => d.EspecialistId == EspecialistId);

            if (MessageListByEspecialistId.Count() > 0)
            {
                return Ok(MessageListByEspecialistId.Select(d => new MessageModel
                {
                    Messageid = d.Messageid,
                    MessageDescription = d.MessageDescription,
                    answer = d.answer,
                    UserId = d.UserId,
                    EspecialistId = d.EspecialistId

                }));
            }
            else
            {
                return Ok("No hay message(s) para el Especialist.");
            }
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Messages/{id}")]
        public async Task<IActionResult> PutMessage(int id, [FromBody] UpdateMessageModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest();

            var variable = await _context.Messages.FirstOrDefaultAsync(d => d.Messageid == id);

            if (variable == null)
                return NotFound();



            variable.MessageDescription = model.MessageDescription;
            variable.answer = model.answer;
            variable.UserId = model.UserId;
            variable.EspecialistId = model.EspecialistId;

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

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Messages")]
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
        [HttpDelete("Messages/{id}")]
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
