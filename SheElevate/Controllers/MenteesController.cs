using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SheElevate.Areas.Identity.Data;
using SheElevate.Data;
using SheElevate.Models;

namespace SheElevate.Controllers
{
    public class MenteesController : Controller
    {
        private readonly SheElevateContext _context;
        private readonly UserManager<SheElevateUser> _userManager;
        private readonly SignInManager<SheElevateUser> _signInManager;


        public MenteesController(SheElevateContext context, UserManager<SheElevateUser> userManager, SignInManager<SheElevateUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            // Fetch the list of mentors from the database
            var mentors = await _context.Mentors.ToListAsync();

            // Pass the mentors to the view
            return View(mentors);
        }
        public IActionResult RegisterMentee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterMentee(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the user based on ASP.NET Identity
                var user = new SheElevateUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Assign the "Mentee" role to the user
                    await _userManager.AddToRoleAsync(user, "Mentee");

                    // Now add the mentee details to the Mentees table
                    var mentee = new Mentee
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        DateOfBirth = model.DateOfBirth
                    };

                    // Add to the database
                    _context.Mentees.Add(mentee);
                    await _context.SaveChangesAsync();

                    // Redirect after successful registration
                    return RedirectToAction("Index", "Home");
                }

                // Handle errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        }
    }

