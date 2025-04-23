using Ensek_Remote_Technical_Test.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ensek_Remote_Technical_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAccounts()
        {
            var accounts = _context.Accounts.ToList();
            return Ok(accounts);
        }
    }
}
