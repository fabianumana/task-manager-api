using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using System.Linq;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static List<TaskItem> tasks = new List<TaskItem>();

        [HttpGet]
        public ActionResult<List<TaskItem>> GetTasks()
        {
            return Ok(tasks);
        }

        [HttpPost]
        public ActionResult AgregarTask([FromBody] TaskItem task)
        {
            task.Id = tasks.Count + 1;
            tasks.Add(task);

            return Ok(tasks);
        }

        //PUT /api/tasks/{id}
        [HttpPut("{id}")]
        public ActionResult ActualizarTask(int id, [FromBody] TaskItem updatedtask)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
               return NotFound();
            }

            task.Name = updatedtask.Name;
            task.IsComplete = updatedtask.IsComplete;

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public ActionResult EliminarTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            tasks.Remove(task);
            return Ok(new { message = "Task deleted successfully"});
        }
    }
}
