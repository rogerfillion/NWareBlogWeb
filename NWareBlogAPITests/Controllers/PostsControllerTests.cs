using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NWareBlogModels;
using System;

namespace NWareBlogAPI.Controllers.Tests
{
    [TestClass()]
    public class PostsControllerTests
    {
        [TestMethod()]
        public void ListTest()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
               .UseInMemoryDatabase("BlogDbForTesting")
               .Options;
            using (var context = new BlogContext(options))
            {
                context.Categories.Add(new Category() { Title = "Category1" });
                context.Categories.Add(new Category() { Title = "Category2" });

                context.Posts.Add(new Post() { Title = "Post1", CategoryId = 1, Content = "Content1", PublicationDate = DateTime.Today });
                context.Posts.Add(new Post() { Title = "Post2", CategoryId = 1, Content = "Content2", PublicationDate = DateTime.Today });
                context.Posts.Add(new Post() { Title = "Post3", CategoryId = 2, Content = "Content3", PublicationDate = DateTime.Today.AddDays(1) });

                context.SaveChanges();

                ActionResult<Post> res = new PostsController(context).List();
                var result = (Microsoft.AspNetCore.Mvc.OkObjectResult)res.Result;
                Assert.AreEqual(result.StatusCode, 200);
            }
        }

        [TestMethod()]
        public void ListEmptyTest()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
               .UseInMemoryDatabase("BlogDbForTesting")
               .Options;
            using (var context = new BlogContext(options))
            {
                context.Categories.Add(new Category() { Title = "Category1" });
                context.Categories.Add(new Category() { Title = "Category2" });

                
                context.SaveChanges();

                ActionResult<Post> res = new PostsController(context).List();
                var result = (Microsoft.AspNetCore.Mvc.NoContentResult)res.Result;
                Assert.AreEqual(result.StatusCode, 204);
            }
        }
        [TestMethod()]
        public void PostIdTest()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase("BlogDbForTesting")
                .Options;
            using (var context = new BlogContext(options))
            {
                context.Categories.Add(new Category() { Title = "Category1" });
                context.Categories.Add(new Category() { Title = "Category2" });

                context.Posts.Add(new Post() { Title = "Post1", CategoryId = 1, Content = "Content1", PublicationDate = DateTime.Today });
                context.Posts.Add(new Post() { Title = "Post2", CategoryId = 1, Content = "Content2", PublicationDate = DateTime.Today });
                context.Posts.Add(new Post() { Title = "Post3", CategoryId = 2, Content = "Content3", PublicationDate = DateTime.Today.AddDays(1) });

                context.SaveChanges();
                ActionResult<Post> res = new PostsController(context).GetPost(1);
                var result = (OkObjectResult)res.Result;
                ActionResult<Post> res2 = new PostsController(context).GetPost(-1);
                var result2 = (NotFoundResult)res2.Result;

                Assert.AreEqual(result.StatusCode, 200);
                Assert.AreEqual(result2.StatusCode, 404);
            }
        }
    }
}