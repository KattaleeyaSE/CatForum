using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CatForum.Context
{
    public class CatContext : DbContext
    {
        public CatContext() : base("CatContext")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Amphur> Amphurs { get; set; }
        public virtual DbSet<Tumbon> Tumbons { get; set; }

        public virtual DbSet<Cat> Cats { get; set; }
        public virtual DbSet<CatBreed> Breeds { get; set; }
        public virtual DbSet<CatCoat> Coats { get; set; }
        public virtual DbSet<CatEyes> Eyes { get; set; }
        public virtual DbSet<CatLifeStage> LifeStages { get; set; }
        public virtual DbSet<CatPattern> Patterns { get; set; }
        public virtual DbSet<CatTail> Tails { get; set; }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostType> PostTypes { get; set; }
        public virtual DbSet<PostDetail> PostDetails { get; set; }
        public virtual DbSet<Report> Reports { get; set; }

        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}