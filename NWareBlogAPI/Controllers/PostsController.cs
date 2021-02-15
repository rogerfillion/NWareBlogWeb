using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NWareBlogModels;

namespace NWareBlogAPI.Controllers
{
    [Route("API/[Controller]")]
    [ApiController]
    public class PostsController : Controller
    {
        private readonly BlogContext _context;

        public PostsController(BlogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<Post> List()
        {
            var posts = _context.Posts
                .Where(x => x.PublicationDate <= DateTime.Now)
                .OrderByDescending(x => x.PublicationDate).ToList();

            if (posts.Count == 0)
            {
                return NoContent();
            }
            return Ok(posts);
        }

        
        [HttpGet("{id}")]
        public ActionResult<Post> GetPost(int id)
        {
            var post = _context.Posts.Find(id);

            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }
    }
}