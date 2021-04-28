using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Models;
using TrickingLibrary.Data;

namespace TrickingLibrary.API.Controllers
{
    [ApiController]
    [Route(("api/tricks"))]
    public class TricksController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public TricksController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IEnumerable<Trick> All() => _appDbContext.Tricks.ToList();

        [HttpGet("{id}")]
        public Trick Get(int id) => _appDbContext.Tricks.FirstOrDefault(t => t.Id.Equals(id));
        
        [HttpGet("{trickId}/submissions")]
        public IEnumerable<Submission> ListSubmissionsForTrick(int trickId) =>
            _appDbContext.Submissions.Where(t => t.TrickId.Equals(trickId));

        [HttpPost]
        public async Task<Trick> Create([FromBody] Trick trick)
        {
            _appDbContext.Add(trick);
            await _appDbContext.SaveChangesAsync();
            return trick;
        }
        
        [HttpPut]
        public async Task<Trick> Update([FromBody] Trick trick)
        {
            if (trick.Id == 0)
            {
                return null;
            }
            _appDbContext.Add(trick);
             await _appDbContext.SaveChangesAsync();
             return trick; 
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var trick = _appDbContext.Tricks.FirstOrDefault(t => t.Id.Equals(id));
           
            if (trick != null)
            {
                trick.IsDeleted = true;
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