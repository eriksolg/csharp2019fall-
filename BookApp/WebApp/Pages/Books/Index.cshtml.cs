using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; }
        
        [FromQuery(Name = "search")]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                var matchedAuthors = await _context.Authors
                    .Where(a => a.FirstName.Contains(SearchString)).Select(a => a.AuthorId).ToListAsync();
                var matchedPublishers = await _context.Publishers
                    .Where(p => p.PublisherName.Contains(SearchString)).Select(p => p.PublisherId).ToListAsync();
                var matchedBooks = await _context.Books
                    .Where(b => b.Title.Contains(SearchString)).Select(b => b.BookId).ToListAsync();

                Book = await _context.Books
                    .Where(b => matchedAuthors.Contains(b.AuthorId) ||
                                matchedPublishers.Contains(b.PublisherId) ||
                                matchedBooks.Contains(b.BookId))
                    .Include(b => b.Author)
                    .Include(b => b.Publisher).ToListAsync();
            }
            else
            {
                Book = await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Publisher).ToListAsync();
            }
            
        }
    }
}
