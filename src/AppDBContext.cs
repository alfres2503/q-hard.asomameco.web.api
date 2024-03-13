using Microsoft.EntityFrameworkCore;
using src.Models;
using src.Utils;
using System.Globalization;

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
        public DbSet<CateringService> CateringService { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateRelations(modelBuilder);

            SeedData(modelBuilder);
        }

        void CreateRelations(ModelBuilder modelBuilder)
        {
            // Relation between Member and Role
            modelBuilder.Entity<Member>()
                .HasOne(m => m.Role)
                .WithMany()
                .HasForeignKey(m => m.IdRole);

            // Relation between Event and Member
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Member)
                .WithMany()
                .HasForeignKey(e => e.IdMember);

            // Relation between Event and Catering Service
            modelBuilder.Entity<Event>()
                .HasOne(e => e.CateringService)
                .WithMany()
                .HasForeignKey(e => e.IdCateringService);

            // Many to many relation between Associate and Event using Attendance as intermediate table
            modelBuilder.Entity<Attendance>()
                .HasKey(a => new { a.IdAssociate, a.IdEvent });

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Associate)
                .WithMany()
                .HasForeignKey(a => a.IdAssociate);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Event)
                .WithMany()
                .HasForeignKey(a => a.IdEvent);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Description = "Admin" },
                new Role { Id = 2, Description = "Member" }
                );

            modelBuilder.Entity<Member>().HasData(
                new Member { Id = 1, IdRole = 1, IdCard = "111111111", FirstName = "Fred", LastName = "Suárez", Email = "lusuarezag@est.utn.ac.cr", Password = Cryptography.EncryptAES("123"), IsActive = true },
                new Member { Id = 2, IdRole = 2, IdCard = "222222222", FirstName = "Pala", LastName = "López", Email = "malopezsa@est.utn.ac.cr", Password = Cryptography.EncryptAES("123"), IsActive = true },
                new Member { Id = 3, IdRole = 2, IdCard = "333333333", FirstName = "Gabo", LastName = "Ulate", Email = "gabulatem@est.utn.ac.cr", Password = Cryptography.EncryptAES("123"), IsActive = true },
                new Member { Id = 4, IdRole = 2, IdCard = "444444444", FirstName = "Fio", LastName = "Salas", Email = "jgonzalez@mail.com", Password = Cryptography.EncryptAES("123"), IsActive = false }
                );

            modelBuilder.Entity<CateringService>().HasData(
                new CateringService { Id = 1, Name = "Catering Soluciones", Email = "info@cateringsoluciones.com", Phone = "2271 7575", IsActive = true },
                new CateringService { Id = 2, Name = "Servicio & Sazón - Catering Service", Email = "info@lacucharaderebe.com", Phone = "7150 9328", IsActive = true },
                new CateringService { Id = 3, Name = "Pasta Pronto Express y Catering", Email = "pastaprontocr@gmail.com", Phone = "4704 8057", IsActive = true },
                new CateringService { Id = 4, Name = "Newrest", Email = "info@newrest.com", Phone = "2437 1768", IsActive = true }
                );

            modelBuilder.Entity<Event>().HasData(
                new Event { Id = 1, IdMember = 2, Name = "Reunión de la junta directiva", Description = "Reunión de la junta directiva para discutir temas importantes", Date = DateOnly.Parse("October 21, 2022", CultureInfo.InvariantCulture), Time = new TimeOnly(7, 23, 11), Place = "Sala de juntas", IdCateringService=1 },
                new Event { Id = 2, IdMember = 3, Name = "Fiesta Mariachi", Description = "Fiestón para esuchar Luis Miguel", Date = DateOnly.Parse("December 23, 2022", CultureInfo.InvariantCulture), Time = new TimeOnly(10, 30, 11), Place = "Sala de juntas", IdCateringService = 3 },
                new Event { Id = 3, IdMember = 2, Name = "Reunión porqué amo a mi esposita", Description = "Reunión recapacitativa", Date = DateOnly.Parse("January 15, 2023", CultureInfo.InvariantCulture), Time = new TimeOnly(14, 00, 11), Place = "Sala de juntas", IdCateringService = 2 }
                );

            modelBuilder.Entity<Associate>().HasData(
                new Associate { Id = 1, IdCard = "555555555", Name = "Luis Gallego", Email = "luis@mail.com", IsActive = true, Phone = "88888888" },
                new Associate { Id = 2, IdCard = "666666666", Name = "Mario Gallego", Email = "mario@mail.com", IsActive = true, Phone = "77777777" },
                new Associate { Id = 3, IdCard = "777777777", Name = "Pedro Fernández", Email = "pfer@mail.com", IsActive = true, Phone = "66666666" }
                );

            modelBuilder.Entity<Attendance>().HasData(
                new Attendance { IdAssociate = 1, IdEvent = 1, ArrivalTime = new TimeOnly(7, 23, 11), isConfirmed = true },
                new Attendance { IdAssociate = 2, IdEvent = 1, ArrivalTime = new TimeOnly(7, 23, 11), isConfirmed = true },
                new Attendance { IdAssociate = 3, IdEvent = 1, ArrivalTime = new TimeOnly(7, 23, 11), isConfirmed = true },
                new Attendance { IdAssociate = 1, IdEvent = 2, ArrivalTime = new TimeOnly(10, 30, 11), isConfirmed = true },
                new Attendance { IdAssociate = 2, IdEvent = 2, ArrivalTime = new TimeOnly(10, 32, 11), isConfirmed = true },
                new Attendance { IdAssociate = 3, IdEvent = 2, ArrivalTime = null, isConfirmed = false },
                new Attendance { IdAssociate = 1, IdEvent = 3, ArrivalTime = null, isConfirmed = false },
                new Attendance { IdAssociate = 2, IdEvent = 3, ArrivalTime = new TimeOnly(14, 00, 11), isConfirmed = true },
                new Attendance { IdAssociate = 3, IdEvent = 3, ArrivalTime = new TimeOnly(14, 00, 11), isConfirmed = true }
                );

        }

    }
}
