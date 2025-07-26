using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationBlueCollar.Data;
using WebApplicationBlueCollar.Models;

namespace WebApplicationBlueCollar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            var jobs = await _context.Jobs.ToListAsync();
            return Ok(jobs);
        }

        // POST: api/jobs
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob([FromBody] Job job)
        {
            if (job == null)
            {
                return BadRequest("Job cannot be null.");
            }

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJobs), new { id = job.Id }, job);
        }
        // Update Job 

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] Job job)
        {
            if (id != job.Id)
            {
                return BadRequest("Job ID mismatch");
            }

            var existingJob = await _context.Jobs.FindAsync(id);
            if (existingJob == null)
            {
                return NotFound();
            }

            // Update only the fields that should be updated
            existingJob.Title = job.Title;
            existingJob.Description = job.Description;
            existingJob.Location = job.Location;
            existingJob.Company = job.Company;
            // Add more fields as needed

            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content is standard for successful PUT
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // OPTIONAL: GET single job by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
                return NotFound();

            return Ok(job);
        }
    }
}
