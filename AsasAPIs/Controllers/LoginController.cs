using Asas.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Asas.AsasHash.Asas.Contracts;
using Asas.AsasHash.Asas.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace AsasAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AsasContext _context;

        private readonly IAsasHashPassword _asasHash;

        public LoginController(AsasContext context, IAsasHashPassword asasHash)
        {
            _context = context;
            _asasHash = asasHash;
        }

        [HttpPost]
        public async Task<IActionResult> login(string email, string password)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest(ModelState);
            }
            
            var CompAcc = await _context.Acc.FirstOrDefaultAsync(u => u.Email == email);
            if (CompAcc == null) 
                return BadRequest(ModelState);

            Hash hash = new Hash
            {
                RawPassword = password,

                GoodHash = new GoodHash
                {
                    IV = CompAcc.IV,
                    Iterations = 5000,
                    Salt = new byte[16],
                 
                }
            };

            var decryption =  _asasHash.VerifyPassword(hash);

            if (CompAcc == null || !decryption.IsSucceeded)
            {
                return Unauthorized(new { Message = "بيانات الدخول غير صحيحة" });
            }

            // var token = GenerateJwtToken(CompAcc); بعدين اسوي الكلاس 

            return Ok(
                new
                {
                    Message = "تم تسجيل الدخول بنجاح"
                }
                
                );









        }
    }

}
