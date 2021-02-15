using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NWareBlogModels;

namespace NWareBlogAPI.Controllers
{
    [Route("API/[Controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly BlogContext _context;

        public CategoriesController(BlogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<Category> List()
        {
            var categories = _context.Categories.ToList();

            if (categories.Count == 0)
            {
                return NoContent();
            }
            return Ok(categories);
        }

        [HttpGet("{id}/Posts")]
        public ActionResult<Post> GetCategoryPost(int id)
        {
            if (_context.Categories.Find(id) == null)
            {
                return NotFound();
            }
            
            var posts = _context.Posts
                .Where(x => x.CategoryId==id && x.PublicationDate <= DateTime.Now)
                .OrderByDescending(x => x.PublicationDate).ToList();

            if (posts.Count==0)
            {
                return NoContent();
            }

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
    }
}
