﻿using SqlServerEntityFramework.Model;
using System.Data.Entity;

namespace SqlServerEntityFramework.Context
{
    internal class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
