using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SQLitePCL;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DAL.AppDbContext _context;

        public int AuthorsCount { get; set; }
        public int PublishersCount { get; set; }
        public int BooksCount { get; set; }
        public int CommentsCount { get; set; }
        
        public IndexModel(ILogger<IndexModel> logger, DAL.AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            AuthorsCount = _context.Authors.Count();
            PublishersCount = _context.Publishers.Count();
            BooksCount = _context.Books.Count();
            CommentsCount = _context.Comments.Count();
        }
    }
}