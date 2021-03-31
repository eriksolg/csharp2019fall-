using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.TodoTasks
{
    public class EditModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public EditModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["TodoTaskCategoryId"] = new SelectList(_context.TodoTaskCategories, "TodoTaskCategoryId", "TodoTaskCategoryName");
           ViewData["TodoTaskPriorityId"] = new SelectList(_context.TodoTaskPriorities, "TodoTaskPriorityId", "TodoTaskPriorityName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TodoTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoTaskExists(TodoTask.TodoTaskId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TodoTaskExists(int id)
        {
            return _context.TodoTasks.Any(e => e.TodoTaskId == id);
        }
    }
}
