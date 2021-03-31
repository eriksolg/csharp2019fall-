using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.TodoTaskPriorities
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TodoTaskPriority TodoTaskPriority { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TodoTaskPriority = await _context.TodoTaskPriorities.FirstOrDefaultAsync(m => m.TodoTaskPriorityId == id);

            if (TodoTaskPriority == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TodoTaskPriority = await _context.TodoTaskPriorities.FindAsync(id);

            if (TodoTaskPriority != null)
            {
                _context.TodoTaskPriorities.Remove(TodoTaskPriority);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
