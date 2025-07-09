using Microsoft.EntityFrameworkCore;
using StudyHub.Entities;

namespace StudyHub.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Post { get; set; }
        public DbSet<Categories> Categories { get; set; }

        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentReply> CommentReply { get; set; }

        public DbSet<React> React { get; set; } 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CommentReply>()
                .HasOne(cr => cr.Comment)
                .WithMany(c => c.Reply)
                .HasForeignKey(cr => cr.Comment_Id)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.User_Id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<React>()
                .HasOne(r => r.Post)
                .WithMany(p=>p.Reacts)
                .HasForeignKey(r=>r.Post_Id) .OnDelete(DeleteBehavior.Restrict);    
               
        }



    }
}
