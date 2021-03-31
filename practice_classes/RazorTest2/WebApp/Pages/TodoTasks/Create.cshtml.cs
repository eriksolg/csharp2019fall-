using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages.TodoTasks
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public SelectList TodoTaskCategorySelectList { get; set; }
        public SelectList TodoTaskPrioritySelectList { get; set; }

        public IActionResult OnGet()
        {
            TodoTaskCategorySelectList =
                new SelectList(_context.TodoTaskCategories, nameof(TodoTaskCategory.TodoTaskCategoryId), nameof(TodoTaskCategory.TodoTaskCategoryName));
            TodoTaskPrioritySelectList =
                new SelectList(_context.TodoTaskPriorities, nameof(TodoTaskPriority.TodoTaskPriorityId), nameof(TodoTaskPriority.TodoTaskPriorityName));
            return Page();
        }

        [BindProperty]
        public TodoTask TodoTask { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TodoTaskCategorySelectList =
                    new SelectList(_context.TodoTaskCategories, "TodoTaskCategoryId", "TodoTaskCategoryName");
                TodoTaskPrioritySelectList =
                    new SelectList(_context.TodoTaskPriorities, "TodoTaskPriorityId", "TodoTaskPriorityName");
                return Page();
            }

            _context.TodoTasks.Add(TodoTask);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
