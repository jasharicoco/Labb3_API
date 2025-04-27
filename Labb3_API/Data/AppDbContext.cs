using Labb3_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3_API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Link> Links { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Persons
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    FirstName = "Alexander",
                    LastName = "Ek",
                    Email = "alexander@ek.se",
                    PhoneNumber = "070-1234567",
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Serhan",
                    LastName = "Gyuler",
                    Email = "serhan@gyuler.uk",
                    PhoneNumber = "070-7654321",
                },
                new Person
                {
                    Id = 3,
                    FirstName = "Petter",
                    LastName = "Boström",
                    Email = "petter@bostrom.se",
                    PhoneNumber = "070-1122334",
                },
                new Person
                {
                    Id = 4,
                    FirstName = "Leon",
                    LastName = "Jashari",
                    Email = "leon@jashari.se",
                    PhoneNumber = "070-5566778",
                },
                new Person
                {
                    Id = 5,
                    FirstName = "Joel",
                    LastName = "Mako",
                    Email = "joel@mako.se",
                    PhoneNumber = "070-9988776",
                }
            );

            // Seed Interests
            modelBuilder.Entity<Interest>().HasData(
                new Interest { Id = 1, InterestName = "Cykling", Description = "Cykla för att uppnå själsligt välmående." },
                new Interest { Id = 2, InterestName = "Bergsklättring", Description = "En utmaning som enbart de allra djärvaste av människor vågar sig på." },
                new Interest { Id = 3, InterestName = "Resa", Description = "För att det är kul." },
                new Interest { Id = 4, InterestName = "Vila", Description = "Konsten att återhämta sig." },
                new Interest { Id = 5, InterestName = "Programmering", Description = "Bygg dina egna applikationer och hemsidor med mera." },
                new Interest { Id = 6, InterestName = "Fotboll", Description = "Kasta bollen och spring för att vinna." },
                new Interest { Id = 7, InterestName = "Matlagning", Description = "Kreativitet i köket!" },
                new Interest { Id = 8, InterestName = "Fysik", Description = "För att förstå världen omkring oss." },
                new Interest { Id = 9, InterestName = "Konst", Description = "Kreativa uttryck för själens bästa." },
                new Interest { Id = 10, InterestName = "Film", Description = "Filmer för att fånga fantasin." }
            );

            // Seed Links (utan FK till Person/Interest)
            modelBuilder.Entity<Link>().HasData(
                new Link { Id = 1, Url = "https://www.cykling.se", Description = "Information om cykling. Hur man gör osv." },
                new Link { Id = 2, Url = "https://www.cykelturer.se", Description = "Information om turer runtom i landet." },
                new Link { Id = 3, Url = "https://www.bergsklattring.se", Description = "Information om bergsklättring." },
                new Link { Id = 4, Url = "https://www.klattraberg.se", Description = "Information om alla faror kopplade till bergsklättring." },
                new Link { Id = 5, Url = "https://www.ving.se", Description = "Hitta dina resor här." },
                new Link { Id = 6, Url = "https://www.apollo.se", Description = "En till sida att hitta sina resor på." },
                new Link { Id = 7, Url = "https://www.flygresor.se", Description = "Sök billiga flyg." },
                new Link { Id = 8, Url = "https://www.meditation.se", Description = "Lär dig vila." },
                new Link { Id = 9, Url = "https://www.fotboll.se", Description = "Allt om fotboll." },
                new Link { Id = 10, Url = "https://www.matlagning.se", Description = "Bästa recepten online." },
                new Link { Id = 11, Url = "https://www.astro.se", Description = "Lär dig fysik genom roliga experiment." },
                new Link { Id = 12, Url = "https://www.konst.se", Description = "Se konst från hela världen." },
                new Link { Id = 13, Url = "https://www.film.se", Description = "Streama filmer och tv-serier." },
                new Link { Id = 14, Url = "https://www.github.com", Description = "Lägg upp dina projekt här." },
                new Link { Id = 15, Url = "https://www.stackoverflow.com", Description = "Lär dig mer om programmering." }
            );

            // Seed Many-to-Many: Person <-> Link
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Links)
                .WithMany(l => l.Persons)
                .UsingEntity(j => j.HasData(
                    new { LinksId = 1, PersonsId = 3 },
                    new { LinksId = 2, PersonsId = 3 },
                    new { LinksId = 3, PersonsId = 4 },
                    new { LinksId = 14, PersonsId = 4 },
                    new { LinksId = 15, PersonsId = 4 },
                    new { LinksId = 4, PersonsId = 5 },
                    new { LinksId = 5, PersonsId = 3 },
                    new { LinksId = 6, PersonsId = 4 },
                    new { LinksId = 7, PersonsId = 5 },
                    new { LinksId = 8, PersonsId = 5 }
                ));

            // Seed Many-to-Many: Interest <-> Link
            modelBuilder.Entity<Interest>()
                .HasMany(i => i.Links)
                .WithMany(l => l.Interests)
                .UsingEntity(j => j.HasData(
                    new { InterestsId = 1, LinksId = 1 },
                    new { InterestsId = 1, LinksId = 2 },
                    new { InterestsId = 2, LinksId = 3 },
                    new { InterestsId = 2, LinksId = 4 },
                    new { InterestsId = 3, LinksId = 5 },
                    new { InterestsId = 3, LinksId = 6 },
                    new { InterestsId = 4, LinksId = 8 },
                    new { InterestsId = 5, LinksId = 14 },
                    new { InterestsId = 5, LinksId = 15 },
                    new { InterestsId = 6, LinksId = 9 },
                    new { InterestsId = 7, LinksId = 10 },
                    new { InterestsId = 8, LinksId = 11 },
                    new { InterestsId = 9, LinksId = 12 },
                    new { InterestsId = 10, LinksId = 13 }
                ));

            // Seed Many-to-Many: Person <-> Interest
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Interests)
                .WithMany(i => i.Persons)
                .UsingEntity(j => j.HasData(
                    new { PersonsId = 3, InterestsId = 1 },
                    new { PersonsId = 3, InterestsId = 3 },
                    new { PersonsId = 4, InterestsId = 2 },
                    new { PersonsId = 4, InterestsId = 5 },
                    new { PersonsId = 5, InterestsId = 6 },
                    new { PersonsId = 5, InterestsId = 7 },
                    new { PersonsId = 5, InterestsId = 8 }
                ));


        }

    }
}
