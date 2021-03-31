using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.TodoTasks
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DetailsModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public TodoTask TodoTask { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TodoTask = await _context.TodoTasks
                .Include(t => t.TodoTaskCategory)
                .Include(t => t.TodoTaskPriority).FirstOrDefaultAsync(m => m.TodoTaskId == id);

            if (TodoTask == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
