using Labb3_API.Data;
using Labb3_API.Models;
using Labb3_API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Labb3_API.Repositories
{
    public class InterestRepository : GenericRepository<Interest>, IInterestRepository
    {
        private readonly AppDbContext _context;

        public InterestRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetInterests>> GetAllInterestsDetailsAsync()
        {
            return await _context.Interests
                .Select(i => new GetInterests
                {
                    Id = i.Id,
                    Name = i.InterestName,
                    Description = i.Description,
                    Persons = i.Persons.Select(p => new GetPersonsFromInterest
                    {
                        Name = $"{p.FirstName} {p.LastName}"
                    }).ToList(),
                    Links = i.Links.Select(l => new GetLinksFromInterest
                    {
                        Id = l.Id,
                        Name = l.Name,
                        URL = l.Url,
                        Description = l.Description,
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<GetInterests> GetInterestDetailsAsync(int id)
        {
            return await _context.Interests
                .Where(i => i.Id == id)
                .Select(i => new GetInterests
                {
                    Name = i.InterestName,
                    Description = i.Description,
                    Persons = i.Persons.Select(p => new GetPersonsFromInterest
                    {
                        Name = $"{p.FirstName} {p.LastName}"
                    }).ToList(),
                    Links = i.Links.Select(l => new GetLinksFromInterest
                    {
                        URL = l.Url,
                        Description = l.Description,
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<List<Link>> GetLinksByIdsAsync(List<int> linkIds)
        {
            return await _context.Links
                .Where(l => linkIds.Contains(l.Id))
                .ToListAsync();
        }

        public async Task<List<Person>> GetPersonsByIdsAsync(List<int> personIds)
        {
            return await _context.Persons
                .Where(p => personIds.Contains(p.Id))
                .ToListAsync();
        }

    }
}
