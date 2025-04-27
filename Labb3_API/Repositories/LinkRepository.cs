using Labb3_API.Data;
using Labb3_API.Models;
using Labb3_API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Labb3_API.Repositories
{
    public class LinkRepository : GenericRepository<Link>, ILinkRepository
    {
        private readonly AppDbContext _context;

        public LinkRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetLinks>> GetAllLinksDetailsAsync()
        {
            return await _context.Links
                .Select(l => new GetLinks
                {
                    Id = l.Id,
                    Url = l.Url,
                    Description = l.Description,
                    Persons = l.Persons.Select(p => new GetPersonsFromLink
                    {
                        Name = $"{p.FirstName} {p.LastName}"
                    }).ToList(),
                    Interests = l.Interests.Select(l => new GetInterestsFromLink
                    {
                        Name = l.InterestName,
                        Description = l.Description
                    }).ToList(),
                }).ToListAsync();
        }

        public async Task<List<Interest>> GetInterestsByIdsAsync(List<int> interestIds)
        {
            return await _context.Interests
                .Where(i => interestIds.Contains(i.Id))
                .ToListAsync();
        }

        public async Task<GetLinks> GetLinkDetailsAsync(int id)
        {
            return await _context.Links
                .Where(l => l.Id == id)
                .Select(l => new GetLinks
                {
                    Url = l.Url,
                    Description = l.Description,
                    Persons = l.Persons.Select(p => new GetPersonsFromLink
                    {
                        Name = $"{p.FirstName} {p.LastName}"
                    }).ToList(),
                    Interests = l.Interests.Select(i => new GetInterestsFromLink
                    {
                        Name = i.InterestName,
                        Description = i.Description
                    }).ToList(),
                }).FirstOrDefaultAsync();  // Använd FirstOrDefaultAsync för att hämta ett enda objekt
        }

        public async Task<List<Person>> GetPersonsByIdsAsync(List<int> personIds)
        {
            return await _context.Persons
                .Where(p => personIds.Contains(p.Id))
                .ToListAsync();
        }
    }
}
