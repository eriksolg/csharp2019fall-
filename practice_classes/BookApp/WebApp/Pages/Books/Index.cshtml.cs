using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;
using WebApp.DTO;

namespace WebApp.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<BookIndexDto> Book { get;set; }
        
        [FromQuery(Name = "search")]
        public string SearchString { get; set; }
        [FromQuery(Name = "reset")]
        public string Reset { get; set; }
        public async Task OnGetAsync()
        {
            if (Reset == "Reset")
            {
                SearchString = null;
            }

            var bookQuery = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Select(a => new BookIndexDto()
                {
                    Book = a,
                    CommentCount = a.Comments.Count,
                    LastComment = ""
                })
                .AsQueryable();
            
            if (!string.IsNullOrEmpty(SearchString))
            {
                var searchString = SearchString.ToLower().Trim();
                bookQuery = bookQuery
                    .Where(b => b.Book.Author.FirstName.ToLower().Contains(searchString) ||
                                b.Book.Author.LastName.ToLower().Contains(searchString) ||
                                b.Book.Publisher.PublisherName.ToLower().Contains(searchString) ||
                                b.Book.Title.ToLower().Contains(searchString));
            }

            bookQuery = bookQuery.OrderBy(b => b.Book.Title);

            Book = await bookQuery.ToListAsync();

        }
    }
}
