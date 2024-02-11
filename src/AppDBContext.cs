using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Member> Member { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Associate> Associate { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Attendance> Attendance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Crear relación entre tablas
            modelBuilder.Entity<Member>()
                .HasOne(m => m.Role)
                .WithMany(r => r.Members)
                .HasForeignKey(m => m.IdRole);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Member)
                .WithMany(m => m.Events)
                .HasForeignKey(e => e.IdMember);

            // Relación muchos a muchos entre Asociados y Eventos usando tabla intermedia Attendance
            modelBuilder.Entity<Associate>()
                .HasMany(a => a.Events)
                .WithMany(e => e.Associates)
                .UsingEntity<Attendance>(
                    l => l.HasOne<Event>(e => e.Event).WithMany(e => e.Attendances).HasForeignKey(e => e.IdEvent),
                    r => r.HasOne<Associate>(e => e.Associate).WithMany(e => e.Attendances).HasForeignKey(e => e.IdAssociate));
        }
    }
}
