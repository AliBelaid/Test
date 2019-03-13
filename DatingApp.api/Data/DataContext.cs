using DatingApp.api.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.api.Data {
  public class DataContext : DbContext {
    public DataContext (DbContextOptions<DataContext> options) : base (options) { }
    public DbSet<Value> Values { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Likes> Likes { get; set; }
    public DbSet<Message> Messages { get; set; }
    protected override void OnModelCreating (ModelBuilder builder) {
      builder.Entity<Likes> ().HasKey (k => new { k.LikerId, k.LikeeId });
      builder.Entity<Likes> ().HasOne (k => k.Likees).WithMany (u => u.Likers).HasForeignKey (i => i.LikeeId).OnDelete (DeleteBehavior.Restrict);
      builder.Entity<Likes> ().HasOne (k => k.Likers).WithMany (u => u.Likees).HasForeignKey (i => i.LikerId).OnDelete (DeleteBehavior.Restrict);;
      builder.Entity<Message> ().HasOne (k => k.Sender).WithMany (u => u.MessageSent).OnDelete (DeleteBehavior.Restrict);
      builder.Entity<Message> ().HasOne (k => k.Recipient).WithMany (u => u.MessageReceived).OnDelete (DeleteBehavior.Restrict);

    }

  }
}