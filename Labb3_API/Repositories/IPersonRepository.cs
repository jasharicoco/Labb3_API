using Labb3_API.Models;
using Labb3_API.Models.DTOs;

namespace Labb3_API.Repositories
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<GetPersons> GetPersonDetailsAsync(int id);

        Task<IEnumerable<GetPersons>> GetAllPersonsDetailsAsync();

        Task<Person> CreatePersonWithInterestsAndLinksAsync(AddPersonRequest newPerson);

        Task<GetPersons> GetInterestsByPersonIdAsync(int personId);

        Task<GetPersons> GetLinksByPersonIdAsync(int personId);

        Task<(string PersonName, string InterestName)?> AddInterestToPersonWithConfirmationAsync(int personId, int interestId);

    }

}
