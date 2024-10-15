using ChatNow_WebAPi.Domains;
using Microsoft.EntityFrameworkCore;

namespace ChatNow_WebAPi.Infra
{
    public class Context : DbContext
    {
        public Context() { }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Se NÃO estiver configurado
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-84UMQCT\\SQLEXPRESS; initial catalog=chatnow; User Id=sa; pwd=Senai@134; TrustServerCertificate = true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friendship>()
                .HasOne(c => c.UserOne)
                .WithMany()
                .HasForeignKey(c => c.IdUserOne)
                .OnDelete(DeleteBehavior.Restrict); // ou NoAction

            modelBuilder.Entity<Friendship>()
                .HasOne(c => c.UserTwo)
                .WithMany()
                .HasForeignKey(c => c.IdUserTwo)
                .OnDelete(DeleteBehavior.Restrict); // ou NoAction
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<UserConversation> UserConversation { get; set; }
    }
}
