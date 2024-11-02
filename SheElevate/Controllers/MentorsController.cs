using Microsoft.AspNetCore.Mvc;
using SheElevate.Models;
using SheElevate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SheElevate.Controllers
{
    public class MentorsController : Controller
    {
        private readonly SheElevateContext _context;
        private readonly ILogger<MentorsController> _logger;

        public MentorsController(SheElevateContext context, ILogger<MentorsController> logger)
        { 
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Mentors> mentors = await _context.Mentors.ToListAsync();
            return View(mentors);
        }

        public async Task<IActionResult> MentorList()
        {
            var mentors = await _context.Mentors.ToListAsync();
            return View(mentors);
        }

        public IActionResult BookSession(int id)
        {
            _logger.LogInformation("Trying to book session with MentorID: " + id);
            var mentor = _context.Mentors.FirstOrDefault(m => m.MentorsID == id);
            if (mentor == null)
            {
                _logger.LogWarning("Mentor not found for ID: " + id);
                return NotFound("Mentor not found.");
            }

            var menteeEmail = User.Identity?.Name; // Null check for User.Identity
            if (menteeEmail == null)
            {
                _logger.LogWarning("User is not logged in.");
                return BadRequest("Mentee email is missing or user is not logged in.");
            }

            var mentee = _context.Mentees.FirstOrDefault(m => m.Email == menteeEmail);
            if (mentee == null)
            {
                _logger.LogWarning("Mentee not found for email: " + menteeEmail);
                return NotFound("Mentee not found.");
            }

            // Create a new booking object with the mentor details
            var booking = new Booking
            {
                MentorsID = mentor.MentorsID,
                Mentors = mentor,
                MenteeID = mentee.MenteeID,
                Mentee = mentee,
                SessionDetails = ""
            };

            return View(booking); // Pass the booking object to the view
        }

        [HttpPost]
        public IActionResult BookSession(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Bookings.Add(booking);
                _context.SaveChanges();
                return RedirectToAction("MentorList");
            }

            return View(booking);
        }

        [Authorize(Roles = "Mentor")]
        public IActionResult MentorDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Mentee")]
        public IActionResult MenteeDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Mentor")]
        public IActionResult AddData()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddData(Mentors mentors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mentors);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mentors);
        }

        public async Task<IActionResult> EditData(int? MentorsID)
        {
            if (MentorsID == null)
            {
                return NotFound();
            }

            var mentors = await _context.Mentors.FindAsync(MentorsID);

            if (mentors == null)
            {
                return BadRequest(MentorsID + " is not found in the table!");
            }

            return View(mentors);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateData(Mentors mentors)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Mentors.Update(mentors);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Mentors");
                }
                return View("EditData", mentors);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        public async Task<IActionResult> DeleteData(int? MentorsID)
        {
            if (MentorsID == null)
            {
                return NotFound();
            }

            var mentors = await _context.Mentors.FindAsync(MentorsID);
            if (mentors == null)
            {
                return BadRequest(MentorsID + " is not found in the list!");
            }

            _context.Mentors.Remove(mentors);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Mentors");
        }
    }
}
