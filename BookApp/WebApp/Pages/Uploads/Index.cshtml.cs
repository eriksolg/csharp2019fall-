using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Uploads
{
    public class Index : PageModel
    {
        private readonly IWebHostEnvironment _env;

        public Index(IWebHostEnvironment env)
        {
            _env = env;
            FileNames = new List<string>();
        }

        public List<string> FileNames { get; set; }
        public void OnGet()
        {
            string webRootPath = _env.WebRootPath + "/uploads";

            if (System.IO.Directory.Exists(webRootPath))
            {
                string[] fileEntries = Directory.GetFiles(webRootPath);
                foreach (var fullPathFileName in fileEntries)
                {
                    var fileName = Path.GetFileName(fullPathFileName);
                    var fileExtension = Path.GetExtension(fileName);
                    if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".gif")
                    {
                        FileNames.Add(fileName);
                    }
                }
            } else
            {
                FileNames.Add("Directory not found");
            }
        }
    }
}