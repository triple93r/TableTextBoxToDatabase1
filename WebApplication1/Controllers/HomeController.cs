using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Additems()
        {
            ViewBag.Student = _context.tblStudent.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Listitems()
        {
            return View(_context.tblStudent.ToList());
        }

        [HttpGet]
        public IActionResult AddListitems()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddListitems([FromBody] List<tblStudent> rowData1)
        {
            if (rowData1 != null && rowData1.Any())
            {
                try
                {
                    // Assuming YourDbContext is your Entity Framework DbContext
                   
                        // Add each student to the database
                        foreach (var student in rowData1)
                        {
                            _context.tblStudent.Add(student);
                        }
                        // Save changes to the database
                        _context.SaveChanges();
                    return Json("Data saved successfully");
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    return BadRequest("Error saving data: " + ex.Message);
                }
            }

            return BadRequest("No data received");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
