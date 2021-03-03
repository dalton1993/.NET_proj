using Microsoft.EntityFrameworkCore;
using Models;

namespace Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserToUser>()
            .HasKey(k => new { k.UserId, k.FollowerId });

            modelBuilder.Entity<UserToUser>()
                .HasOne(l => l.User)
                .WithMany(a => a.Followers)
                .HasForeignKey(l => l.UserId); 

            modelBuilder.Entity<UserToUser>()
                .HasOne(l => l.Follower)
                .WithMany(a => a.Following)
                .HasForeignKey(l => l.FollowerId);

            modelBuilder.Entity<UserCommunity>()
                .HasKey(k => new { k.UserId, k.CommunityId });

            modelBuilder.Entity<UserCommunity>()
                .HasOne(a => a.User)
                .WithMany(a => a.CommunityMember)
                .HasForeignKey(k => k.UserId);

            modelBuilder.Entity<UserCommunity>()
                .HasOne(b => b.Community)
                .WithMany(b => b.CommunityMember)
                .HasForeignKey(k => k.CommunityId); 
        }
        
        public DbSet<TodoModel> Todos {get; set;}
        public DbSet<User> Users {get; set;}
        public DbSet<Post> Posts {get; set;}
        public DbSet<PostComments> Comments {get;set;}
        public DbSet<CommentReply> Replies {get;set;}
        public DbSet<UserToUser> Relationships {get; set;}
        public DbSet<Community> Communities {get;set;}
        public DbSet<UserCommunity> UserCommunities {get;set;}
    }
}