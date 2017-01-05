using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DMCoreV2.DataAccess;
using DMCoreV2.DataAccess.Models;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DMCoreV2.api
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly ILogger _logger;

        private DMDbContext _dmDbContext;

        public UserController(
            DMDbContext dmContext,
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _dmDbContext = dmContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<UserController>();
        }

        // GET: api/values
        [HttpGet]
        public IQueryable<AuthUser> Get()
        {
            return _dmDbContext.Users;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public AuthUser Get(string email)
        {
            return _dmDbContext.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public async Task<AuthUser> Post(string email, [FromBody]AuthUser authUser)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            authUser.Email = user.Email;
            authUser.UserName = user.UserName;
            authUser.EmailConfirmed = user.EmailConfirmed;

            return authUser;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return new ObjectResult("not found!");

            await _userManager.DeleteAsync(user);

            return Ok();
        }
    }
}
