using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NWareBlogModels;

namespace NWareBlogWeb.Pages.Posts
{
    public class AddModel : PageModel
    {
        private readonly NWareBlogModels.BlogContext _context;
        public SelectList CategoriesOptions { get; set; }

        public AddModel(NWareBlogModels.BlogContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            CategoriesOptions = new SelectList(_context.Categories.ToList(), nameof(NWareBlogModels.Category.Id), nameof(NWareBlogModels.Category.Title));
            return Page();
        }

        [BindProperty]
        public NWareBlogModels.Post Post { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public IEnumerable<NWareBlogModels.Category> Categories() {
            return _context.Categories.ToList();
        
        }
    }
}
