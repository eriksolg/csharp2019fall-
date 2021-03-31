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
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<TodoTaskPriority> TodoTaskPriority { get;set; }

        public async Task OnGetAsync()
        {
            TodoTaskPriority = await _context.TodoTaskPriorities.ToListAsync();
        }
    }
}
