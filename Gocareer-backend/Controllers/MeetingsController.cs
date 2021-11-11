using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gocareer.Domain;
using Gocareer.Infrastructure;
using Gocareer_backend.Models.Meeting;

namespace Gocareer_backend.Controllers
{
    [Route("api")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly DbContextGocareer _context;

        public MeetingsController(DbContextGocareer context)
        {
            _context = context;
        }

        // GET: api/Meetings
        [HttpGet("Meetings")]
        public async Task<IEnumerable<MeetingModel>> GetMeetings()
        {
            var meetingsList = await _context.Meetings.ToListAsync();

            return meetingsList.Select(m => new MeetingModel
            {
                MeetingId = m.MeetingId,
                Date = m.Date,
                Hour = m.Hour,
                UserId = m.UserId,
                EspecialistId = m.EspecialistId
            });
        }

        // GET: api/Meetings/5
        [HttpGet("Meetings/{id}")]
        public async Task<IActionResult> GetMeetingByid(int id)
        {
            var meeting = await _context.Meetings.FindAsync(id);

            if (meeting == null)
                return NotFound();

            return Ok(new MeetingModel
            {
                MeetingId = meeting.MeetingId,
                Date = meeting.Date,
                Hour = meeting.Hour,
                UserId = meeting.UserId,
                EspecialistId = meeting.EspecialistId
            });
        }

        [HttpGet("Especialists/{EspecialistId}/Meetings")]
        public async Task<ActionResult<Meeting>> GetMeetingsByEspecialistId(int EspecialistId)
        {
            IEnumerable<Meeting> meetingList = await _context.Meetings.ToListAsync();

            var MeetingListByEspecialistId = meetingList.ToList().Where(d => d.EspecialistId == EspecialistId);

            if (MeetingListByEspecialistId.Count() > 0)
            {
                return Ok(MeetingListByEspecialistId.Select(d => new MeetingModel
                {
                    MeetingId = d.MeetingId,
                    Date = d.Date,
                    Hour = d.Hour,
                    UserId = d.UserId,
                    EspecialistId = d.EspecialistId

                }));
            }
            else
            {
                return Ok("No hay meeting(s) para el Especialist.");
            }
        }

        // PUT: api/Meetings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMeeting(int id, Meeting meeting)
        //{
        //    if (id != meeting.MeetingId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(meeting).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MeetingExists(id))
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

        // POST: api/Meetings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Meetings")]
        public async Task<IActionResult> PostMeeting([FromBody] CreateMeeting model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Meeting meeting = new Meeting
            {
                Date = model.Date,
                Hour = model.Hour,
                UserId = model.UserId,
                EspecialistId = model.EspecialistId
            };
            _context.Meetings.Add(meeting);
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

        // DELETE: api/Meetings/5
        [HttpDelete("Meetings/{id}")]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            var existing = await _context.Meetings.FindAsync(id);
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

        private bool MeetingExists(int id)
        {
            return _context.Meetings.Any(e => e.MeetingId == id);
        }
    }
}
