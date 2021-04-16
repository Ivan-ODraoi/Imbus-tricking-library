using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.API.Models;

namespace TrickingLibrary.API.Controllers
{
    [ApiController]
    [Route(("api/tricks"))]
    public class TricksController : ControllerBase
    {
        private readonly TrickyStore _trickyStore;

        public TricksController(TrickyStore trickyStore)
        {
            _trickyStore = trickyStore;
        }

        // /api/tricks
        [HttpGet]
        public IActionResult All() => Ok(_trickyStore.All);

        // /api/tricks/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(_trickyStore.All.FirstOrDefault(t => t.Id.Equals(id)));

        // /api/tricks
        [HttpPost]
        public IActionResult Create([FromBody] Trick trick)
        {
            _trickyStore.Add(trick);
            return Ok();
        }

        // /api/tricks
        [HttpPut]
        public IActionResult Update([FromBody] Trick trick)
        {
            throw new NotImplementedException();
        }

        // /api/tricks
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}