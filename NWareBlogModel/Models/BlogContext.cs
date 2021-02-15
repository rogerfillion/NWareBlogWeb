using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NWareBlogModels

{
    public class BlogContext: DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
            
     
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
       


    }
}

