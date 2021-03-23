using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.Comments
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Comment> Comment { get;set; }
        
        [FromQuery(Name = "book")]
        public int BookId { get; set; }

        public async Task OnGetAsync()
        {
            Comment = await _context.Comments
                .Include(c => c.Book)
                .Where(c => c.Book.BookId.Equals(BookId)).ToListAsync();
        }
    }
}
