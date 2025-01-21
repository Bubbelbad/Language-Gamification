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
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);

            builder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Challenge>()
                .HasMany(c => c.Questions)
                .WithOne(q => q.Challenge)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Score>()
                .HasOne(s => s.User)
                .WithMany(u => u.Scores)
                .HasForeignKey(s => s.UserId);

            builder.Entity<UserChallenge>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserChallenges)
                .HasForeignKey(uc => uc.UserId);

            builder.Entity<UserChallenge>()
                .HasOne(uc => uc.Challenge)
                .WithMany(c => c.UserChallenges)
                .HasForeignKey(uc => uc.ChallengeId);

            builder.Entity<UserChallenge>()
                .Property(uc => uc.Score)
                .IsRequired(false);

            builder.Entity<UserChallenge>()
                .Property(uc => uc.CompletedAt)
                .IsRequired(false);



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
