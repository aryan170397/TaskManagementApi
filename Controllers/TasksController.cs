using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Data;
using TaskManagementApi.Models;
using System.Linq;

namespace TaskManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        private bool ValidateToken()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(token))
                return false;

            // Very simple validation: token must end with "-token"
            return token.EndsWith("-token");
        }

        // POST /api/tasks → Create a new task
        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskItem task)
        {
            if (!ValidateToken())
                return Unauthorized(new { message = "Invalid or missing token" });

            _context.TaskItems.Add(task);
            _context.SaveChanges();
            return Ok(task);
        }

        // GET /api/tasks/{id} → Get task by ID
        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            if (!ValidateToken())
                return Unauthorized(new { message = "Invalid or missing token" });

            var task = _context.TaskItems.Find(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // GET /api/tasks/user/{userId} → Get tasks assigned to a specific user
        [HttpGet("user/{userId}")]
        public IActionResult GetTasksByUser(int userId)
        {
            if (!ValidateToken())
                return Unauthorized(new { message = "Invalid or missing token" });

            var tasks = _context.TaskItems
                .Where(t => t.AssignedUserId == userId)
                .ToList();

            return Ok(tasks);
        }
    }
}
