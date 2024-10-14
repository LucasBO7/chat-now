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
            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.UserOne)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // ou NoAction

            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.UserTwo)
                .WithMany()
                .HasForeignKey(c => c.UserTwoId)
                .OnDelete(DeleteBehavior.Restrict); // ou NoAction
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
