using System;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class Test01 : PageModel
    {
        public string Greeting { get; set; } = "Hello, world!";

        [BindProperty(SupportsGet = true)]
        public string PersonName { get; set; }
        
        [BindProperty]
        public Person Person { get; set; }
        
        public void OnGet(string foobar)
        {
            Greeting = Greeting + " " + DateTime.Now + PersonName;

            Person = new Person()
            {
                FirstName = "foo",
                LastName = "bar"
            };
        }

        public void OnPost()
        {
            Greeting = Greeting + " onpost " + PersonName;
            
        }
    }
}