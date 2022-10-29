using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Core.Security.Entities;

namespace Persistance.Context
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(a =>
            {
                a.ToTable("Movies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Title).HasColumnName("Title");
                a.Property(p => p.Adult).HasColumnName("Adult");
                a.Property(p => p.Backdrop_path).HasColumnName("Backdrop_path");
                a.Property(p => p.Media_type).HasColumnName("Media_type");
                a.Property(p => p.Original_language).HasColumnName("Original_language");
                a.Property(p => p.Original_title).HasColumnName("Original_title");
                a.Property(p => p.Overview).HasColumnName("Overview");
                a.Property(p => p.Popularity).HasColumnName("Popularity");
                a.Property(p => p.Poster_path).HasColumnName("Poster_path");
                a.Property(p => p.Release_date).HasColumnName("Release_date");
                a.Property(p => p.Video).HasColumnName("Video");
                a.Property(p => p.Vote_average).HasColumnName("Vote_average");
                a.Property(p => p.Vote_count).HasColumnName("Vote_count");
                //a.Property(p => p.Genre_ids).HasColumnName("Genre_ids");

                a.HasMany(p => p.Comments);
            });

            modelBuilder.Entity<Comment>(a =>
            {
                a.ToTable("Comments").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.MovieId).HasColumnName("MovieId");
                a.Property(p => p.Author).HasColumnName("Author");
                a.Property(p => p.Content).HasColumnName("Content");
                a.Property(p => p.Created_at).HasColumnName("Created_at");
                a.Property(p => p.CommentId).HasColumnName("CommentId");
                a.Property(p => p.updated_at).HasColumnName("Updated_at");
                a.Property(p => p.url).HasColumnName("Url");
                a.Property(p => p.Point).HasColumnName("Point");
                a.HasOne(p => p.Movie);
                //a.HasOne(p => p.Author_details);

            });

            Movie[] movieEntitySeeds = { new(1,false, "/du0zunHPR3uAT2hTyqPKL1jIbtH.jpg", "movie","en", "Winter's Bone", "17 year-old Ree Dolly setsappeared.", 9.157, "/a0qhPkNlxLfsf5B2jFyI1Pp04XV.jpg", "2010-06-11", "Winter's Bone", false,5,752)};
            modelBuilder.Entity<Movie>().HasData(movieEntitySeeds);


            //Model[] modelEntitySeeds = { new(1, 1, "Series 4", 1500, ""), new(2, 1, "Series 3", 1200, ""), new(3, 2, "A180", 1000, "") };
            //modelBuilder.Entity<Model>().HasData(modelEntitySeeds);
        }
    }
}
