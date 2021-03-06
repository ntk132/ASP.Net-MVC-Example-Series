﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebApp.Models;

namespace WebApp.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base ("WebAppDB")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserMeta> UserMetas { get; set; }
        public DbSet<PostMeta> PostMetas { get; set; }
        public DbSet<ProductMeta> ProductMetas { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentMeta> CommentMetas { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<TermMeta> TermMetas { get; set; }
        public DbSet<TermRelation> TermRelations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}