using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NWareBlogModels;

namespace NWareBlogAPI.Controllers.Tests
{
    [TestClass()]
    public class CategoriesControllerTests
    {

        [TestMethod()]
        public void CategoriesListTest()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase("BlogDbForTesting")
                .Options;
            using (var context = new BlogContext(options))
            {
                context.Categories.Add(new Category() { Title = "Category1" });
                context.Categories.Add(new Category() { Title = "Category2" });

                context.SaveChanges();
                ActionResult<Category> res = new CategoriesController(context).List();
                var result = (Microsoft.AspNetCore.Mvc.ObjectResult) res.Result;

                Assert.AreEqual(result.StatusCode,200);
            }
        }
        [TestMethod()]
        public void CategoriesListEmptyTest()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase("BlogDbForTesting")
                .Options;
            using (var context = new BlogContext(options))
            {
                ActionResult<Category> res = new NWareBlogAPI.Controllers.CategoriesController(context).List();
                var result = (Microsoft.AspNetCore.Mvc.NoContentResult)res.Result;
                Assert.AreEqual(result.StatusCode, 204);
            }
        }


        [TestMethod()]
        public void CategoriesIdTest()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase("BlogDbForTesting")
                .Options;
            using (var context = new BlogContext(options))
            {
                context.Categories.Add(new Category() { Title = "Category1" });
                context.Categories.Add(new Category() { Title = "Category2" });

                context.SaveChanges();
                ActionResult<Category> res = new CategoriesController(context).GetCategory(1);
                var result = (OkObjectResult) res.Result;
                
                ActionResult<Category> res2 = new CategoriesController(context).GetCategory(-1);
                var result2 = (NotFoundResult)res2.Result;

                Assert.AreEqual(result.StatusCode, 200);
                Assert.AreEqual(result2.StatusCode, 404);
            }
        }

        [TestMethod()]
        public void CategoriesGetPostTest()
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
                ActionResult<Post> res = new NWareBlogAPI.Controllers.CategoriesController(context).GetCategoryPost(1);
                var result = (Microsoft.AspNetCore.Mvc.OkObjectResult)res.Result;

                ActionResult<Post> res2 = new NWareBlogAPI.Controllers.CategoriesController(context).GetCategoryPost(-1);
                var result2 = (Microsoft.AspNetCore.Mvc.NotFoundResult)res2.Result;

                ActionResult<Post> res3 = new NWareBlogAPI.Controllers.CategoriesController(context).GetCategoryPost(2);
                var result3 = (Microsoft.AspNetCore.Mvc.NoContentResult)res3.Result;

                Assert.AreEqual(result.StatusCode, 200);
                Assert.AreEqual(result2.StatusCode, 404);
                Assert.AreEqual(result3.StatusCode, 204);
            }
        }

        
    }
}