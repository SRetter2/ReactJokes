using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ReactJokes.Data
{
    public class JokeContext : DbContext
    {

        private readonly string _connectionString;

        public JokeContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            modelBuilder.Entity<UserLikedJokes>()
                .HasKey(ulj => new { ulj.JokeId, ulj.UserId });

            modelBuilder.Entity<UserLikedJokes>()
                .HasOne(ulj => ulj.Joke)
                .WithMany(j => j.UserLikedJokes)
                .HasForeignKey(j => j.JokeId);

            modelBuilder.Entity<UserLikedJokes>()
                .HasOne(ulj => ulj.User)
                .WithMany(u => u.UserLikedJokes)
                .HasForeignKey(j => j.UserId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Joke> Jokes { get; set; }
        public DbSet<UserLikedJokes> UserLikedJokes { get; set; }

    }
}
