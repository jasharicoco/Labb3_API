using Labb3_API.Data;
using Labb3_API.Models;
using Labb3_API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Labb3_API.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetPersons>> GetAllPersonsDetailsAsync()
        {
            var persons = await _context.Persons
                .Select(p => new GetPersons
                {
                    Id = p.Id,
                    Name = p.FirstName + " " + p.LastName,
                    Telephone = p.PhoneNumber,
                    Email = p.Email,
                    Interests = p.Interests.Select(interest => new GetInterestsFromPerson
                    {
                        Name = interest.InterestName,
                        Description = interest.Description,
                        Links = p.Links
                            .Where(l => l.Interests.Any(i => i.Id == interest.Id)) // Filtrera länkarna för rätt intresse
                            .Select(l => new GetLinksFromPerson
                            {
                                Name = l.Name,
                                URL = l.Url,
                                Description = l.Description
                            }).ToList()
                    }).ToList()
                }).ToListAsync();

            return persons;
        }

        public async Task<GetPersons> GetPersonDetailsAsync(int id)
        {
            return await _context.Persons
                .Where(p => p.Id == id)
                .Select(p => new GetPersons
                {
                    Name = p.FirstName + " " + p.LastName,
                    Telephone = p.PhoneNumber,
                    Email = p.Email,
                    Interests = p.Interests.Select(i => new GetInterestsFromPerson
                    {
                        Name = i.InterestName,
                        Description = i.Description,
                        Links = i.Links
                            .Where(l => l.Persons.Any(per => per.Id == p.Id))
                            .Select(l => new GetLinksFromPerson
                            {
                                URL = l.Url,
                                Description = l.Description
                            }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Person> CreatePersonWithInterestsAndLinksAsync(AddPersonRequest newPerson)
        {
            // Validera indata
            if (string.IsNullOrWhiteSpace(newPerson.Name) || string.IsNullOrWhiteSpace(newPerson.Email))
            {
                throw new ArgumentException("Name and Email are required.");
            }

            // Skapa person
            var personEntity = new Person
            {
                FirstName = newPerson.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(),
                LastName = newPerson.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries).LastOrDefault(),
                Email = newPerson.Email,
                PhoneNumber = newPerson.Telephone,
                Interests = new List<Interest>(),
                Links = new List<Link>()
            };

            // Koppla intressen
            if (newPerson.InterestIds != null && newPerson.InterestIds.Any())
            {
                var invalidInterestIds = new List<int>();
                foreach (var interestId in newPerson.InterestIds)
                {
                    var interest = await _context.Interests.FindAsync(interestId);
                    if (interest != null)
                    {
                        personEntity.Interests.Add(interest);
                    }
                    else
                    {
                        invalidInterestIds.Add(interestId);
                    }
                }

                if (invalidInterestIds.Any())
                {
                    throw new ArgumentException($"Invalid interest IDs: {string.Join(", ", invalidInterestIds)}");
                }
            }

            // Koppla länkar
            if (newPerson.LinkIds != null && newPerson.LinkIds.Any())
            {
                var invalidLinkIds = new List<int>();
                foreach (var linkId in newPerson.LinkIds)
                {
                    var link = await _context.Links.FindAsync(linkId);
                    if (link != null)
                    {
                        personEntity.Links.Add(link);
                    }
                    else
                    {
                        invalidLinkIds.Add(linkId);
                    }
                }

                if (invalidLinkIds.Any())
                {
                    throw new ArgumentException($"Invalid link IDs: {string.Join(", ", invalidLinkIds)}");
                }
            }

            // Lägg till personen i databasen
            await _context.Persons.AddAsync(personEntity);
            await _context.SaveChangesAsync();

            return personEntity;
        }

        public async Task<GetPersons> GetInterestsByPersonIdAsync(int personId)
        {
            return await _context.Persons
                .Where(p => p.Id == personId)
                .Select(p => new GetPersons
                {
                    Id = p.Id,
                    Name = p.FirstName + " " + p.LastName,
                    Interests = p.Interests.Select(i => new GetInterestsFromPerson
                    {
                        Name = i.InterestName,
                        Description = i.Description,
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }


        public async Task<GetPersons> GetLinksByPersonIdAsync(int personId)
        {
            return await _context.Persons
                .Where(p => p.Id == personId)
                .Select(p => new GetPersons
                {
                    Id = p.Id,
                    Name = p.FirstName + " " + p.LastName,
                    Links = p.Links.Select(l => new GetLinksFromPerson
                    {
                        Name = l.Name,
                        URL = l.Url,
                        Description = l.Description
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<(string PersonName, string InterestName)?> AddInterestToPersonWithConfirmationAsync(int personId, int interestId)
        {
            var person = await _context.Persons.Include(p => p.Interests)
                                               .FirstOrDefaultAsync(p => p.Id == personId);
            var interest = await _context.Interests.FindAsync(interestId);

            if (person == null || interest == null) return null;

            if (!person.Interests.Contains(interest))
            {
                person.Interests.Add(interest);
                await _context.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            var personName = $"{person.FirstName} {person.LastName}";
            var interestName = interest.InterestName;

            return (personName, interestName);
        }


    }

}