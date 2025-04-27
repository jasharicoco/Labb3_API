using Labb3_API.Models;
using Labb3_API.Models.DTOs;

namespace Labb3_API.Repositories
{
    public interface ILinkRepository : IGenericRepository<Link>
    {
        Task<GetLinks> GetLinkDetailsAsync(int id);

        Task<IEnumerable<GetLinks>> GetAllLinksDetailsAsync();

        Task<List<Interest>> GetInterestsByIdsAsync(List<int> interestIds);

        Task<List<Person>> GetPersonsByIdsAsync(List<int> personIds);

    }
}
