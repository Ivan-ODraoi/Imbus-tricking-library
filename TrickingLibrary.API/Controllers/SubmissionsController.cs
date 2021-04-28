using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Data;
using TrickingLibrary.Models;


namespace TrickingLibrary.API.Controllers
{
    [ApiController]
    [Route(("api/submissions"))]
    public class SubmissionsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public SubmissionsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IEnumerable<Submission> All() => _appDbContext.Submissions.ToList();

        [HttpGet("{id}")]
        public Submission Get(int id) => _appDbContext.Submissions.FirstOrDefault(t => t.Id.Equals(id));
        
        [HttpPost]
        public async Task<Submission> Create([FromBody] Submission submission)
        {
            _appDbContext.Add(submission);
            await _appDbContext.SaveChangesAsync();
            return submission;
        }
        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Submission submission)
        {
            if (submission.Id == 0)
            {
                return null;
            }
            _appDbContext.Add(submission);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            var submission = _appDbContext.Submissions.FirstOrDefault(t => t.Id.Equals(id));
           
            if (submission != null)
            {
                submission.IsDeleted = true;
                await _appDbContext.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}