using BulletinBoard.Data.Models;
using BulletinBoard.Data.Interfaces;
using BulletinBoard.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementRepository _repository;

        public AnnouncementsController(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAll()
        {
            var announcements = await _repository.GetAllAsync();
            return Ok(announcements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> GetById(int id)
        {
            var announcement = await _repository.GetByIdAsync(id);
            if (announcement == null)
                return NotFound();
            return Ok(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Announcement announcement)
        {
            await _repository.AddAsync(announcement);
            return Ok("Created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Announcement announcement)
        {
            if (id != announcement.Id)
                return BadRequest();

            await _repository.UpdateAsync(announcement);
            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return Ok("Deleted");
        }
    }
}
