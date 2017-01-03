using DMCoreV2.Areas.Admin.ViewModels;
using DMCoreV2.DataAccess;
using DMCoreV2.DataAccess.Models;
using DMCoreV2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCoreV2.Areas.Admin.Controllers
{
    [Area("Admin")]    
    [Route("admin/[controller]")]
    // [Authorize(Roles = "admin")] 
    public class UserController:Controller
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly IMailService _emailSender;
        private readonly ISmsService _smsSender;
        private readonly ILogger _logger;

        private DMDbContext _dmDbContext;

        public UserController(
            DMDbContext dmContext,
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            IMailService emailSender,
            ISmsService smsSender,
            ILoggerFactory loggerFactory)
        {
            _dmDbContext = dmContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<UserController>();
        }

        //
        // GET: /Account/Login
        [HttpGet, Route("")]
        public IActionResult Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            List<AuthUser> authUsers = _dmDbContext.Users.ToList();
            return View(new UserIndex
            {
                Users = authUsers
            });
        }

        [HttpGet, Route("newuser")]
        public IActionResult NewUser()
        {
            return View(new UserNew
            {
            });
        }

        [HttpPost, Route("newuser")]
        public async Task<IActionResult> NewUser(UserNew form, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(form.Email) == null)
                {
                    var user = new AuthUser();
                    user.UserName = form.Email;
                    user.Email = form.Email;
                    user.EmailConfirmed = form.EmailConfirmed;
                    user.NormalizedEmail = form.Email.ToUpper();
  
                    await _userManager.CreateAsync(user, form.Password);

                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else { return Redirect(returnUrl); }
                }
                else
                {
                    ModelState.AddModelError("", "User creation failed: email must be unique!");
                }
            }
            return View();
        }

        [HttpGet, Route("edituser")]
        public async Task<IActionResult> EditUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            return View(new UserEdit
            {
                Id = Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed
            });
        }

        [HttpPost, Route("edituser")]
        public async Task<IActionResult> EditUser(UserEdit form,  string returnUrl)
        {
            var user = await _userManager.FindByIdAsync(form.Id);
            var userByEmail = await _userManager.FindByEmailAsync(form.Email);
            if (userByEmail!=null && userByEmail.Id != form.Id)
            {
                ModelState.AddModelError("", "User update failed: user with the same email already exist!");
                return View();
            }

            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    user.UserName = form.Email;
                    user.Email = form.Email;
                    user.EmailConfirmed = form.EmailConfirmed;
                    await _userManager.UpdateAsync(user);

                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else { return Redirect(returnUrl); }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User update failed: user not found!");
                }
            }
            return View();
        }

        [HttpGet, Route("deleteuser")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "User to delete not found!");
                }
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet, Route("resetpassword")]
        public async Task<ActionResult> ResetPassword(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("Index", "User");
            }

            return View(new UserResetPassword
            {
                Email = user.Email,
                Password=string.Empty,
                ConfirmPassword=string.Empty,
                Code=string.Empty
            });
        }

        [HttpPost, Route("resetpassword")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(UserResetPassword form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            var user = await _userManager.FindByEmailAsync(form.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                ModelState.AddModelError("", "User to reset password not found!");
                return View();
            }

            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, code, form.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("index", "User");
            }
            else
            {
                ModelState.AddModelError("", "Reset Password failed: ");
            }
 
            return View();
        }
    }
}
