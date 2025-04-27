using Labb3_API.Models;
using Labb3_API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Labb3_API.Repositories
{
    public interface IInterestRepository : IGenericRepository<Interest>
    {
        Task<GetInterests> GetInterestDetailsAsync(int id);

        Task<IEnumerable<GetInterests>> GetAllInterestsDetailsAsync();

        Task<List<Link>> GetLinksByIdsAsync(List<int> linkIds);

        Task<List<Person>> GetPersonsByIdsAsync(List<int> personIds);

    }
}
