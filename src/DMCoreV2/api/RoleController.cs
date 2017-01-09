﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DMCoreV2.DataAccess;
using DMCoreV2.DataAccess.Models;
using Microsoft.Extensions.Logging;

namespace DMCoreV2.api
{
    [Route("api/[controller]")]
    public class RoleController : Controller
    {

        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly RoleManager<AuthRole> _roleManager;
        private readonly ILogger _logger;

        private DMDbContext _dmDbContext;

        public RoleController(
            DMDbContext dmContext,
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            RoleManager<AuthRole> roleManager,
            ILoggerFactory loggerFactory)
        {
            _dmDbContext = dmContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = loggerFactory.CreateLogger<UserController>();
        }
        // GET: api/values
        [HttpGet]
        public IQueryable<AuthRole> Get()
        {
            return _dmDbContext.Roles;
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public AuthRole Get(string id)
        {
            return _dmDbContext.Roles.Where(r => r.Id == id).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(string email, [FromBody]AuthRole authRole)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(authRole.Id);
                if (role == null) return null;

                role.Name = authRole.Name;
                role.Description = authRole.Description;
                await _roleManager.UpdateAsync(role);
                return Created($"api/role/{authRole.Name}", authRole);
            }

            return BadRequest(ModelState);
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
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null) return BadRequest("Role to delete not found!");

                    //Remove All Users from current Role
                    var users = await _userManager.GetUsersInRoleAsync(role.Name);
                    if (users != null)
                    {
                        foreach (var u in users)
                        {
                            await _userManager.RemoveFromRoleAsync(u, role.Name);
                        }
                    }
                //await _roleManager.DeleteAsync(role);
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}
