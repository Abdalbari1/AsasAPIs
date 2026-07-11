using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsasAPIs.Data;
using AsasAPIs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace AsasAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AsasContext _context;

        public HomeController(AsasContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string Name, string Email, string Mass)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            Issue issue = new Issue();
            issue.Name = Name;
            issue.Email = Email;
            issue.Message = Mass;
            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();
            return Ok(200);

        }
    }
}
