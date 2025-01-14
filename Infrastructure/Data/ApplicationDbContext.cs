using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public new DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Relationships

            builder.Entity<Answer>()
                .HasOne(a => a.question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);

            builder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Challenge>()
                .HasMany(c => c.Questions)
                .WithOne(q => q.Challenge)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Score>()
                .HasOne(s => s.user)
                .WithMany(u => u.Scores)
                .HasForeignKey(s => s.UserId);


            // Configure automatic Id generation

            builder.Entity<Answer>()
                .HasKey(e => e.Id);
            builder.Entity<Answer>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Question>()
                .HasKey(e => e.Id);
            builder.Entity<Question>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Challenge>()
                .HasKey(e => e.Id);
            builder.Entity<Challenge>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<UserChallenge>()
                .HasKey(e => e.Id);
            builder.Entity<UserChallenge>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Score>()
                .HasKey(e => e.Id);
            builder.Entity<Score>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
