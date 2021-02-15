using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NWareBlogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NWareBlogWeb.Pages
{
    public class IndexModel : PageModel
    {
        public readonly BlogContext context;
        public IEnumerable<NWareBlogModels.Category> Categories { get; set; }
        public List<NWareBlogModels.Post> Posts { get; set; }

        public IndexModel(BlogContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            Categories = this.context.Categories.ToList();
            Posts = this.context.Posts.ToList();
        }

        

    }
}
