using System;
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
        public Trick Get(string id) => _appDbContext.Tricks.FirstOrDefault(t => t.Id.Equals(id,StringComparison.InvariantCultureIgnoreCase));
        
        [HttpGet("{trickId}/submissions")]
        public IEnumerable<Submission> ListSubmissionsForTrick(string trickId) =>
            _appDbContext.Submissions.Where(t => t.TrickId.Equals(trickId, StringComparison.InvariantCultureIgnoreCase))
                                     .ToList();

        [HttpPost]
        public async Task<Trick> Create([FromBody] Trick trick)
        {
            trick.Id = trick.Name.Replace(" ", "-").ToLowerInvariant();
            _appDbContext.Add(trick);
            await _appDbContext.SaveChangesAsync();
            return trick;
        }
        
        [HttpPut]
        public async Task<Trick> Update([FromBody] Trick trick)
        {
            if (string.IsNullOrEmpty(trick.Id))
            {
                return null;
            }
            _appDbContext.Add(trick);
             await _appDbContext.SaveChangesAsync();
             return trick; 
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var trick = _appDbContext.Tricks.FirstOrDefault(t => t.Id.Equals(id,StringComparison.InvariantCultureIgnoreCase));
           
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