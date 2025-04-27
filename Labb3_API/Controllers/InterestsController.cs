using Labb3_API.Models;
using Labb3_API.Models.DTOs;
using Labb3_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Labb3_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly IInterestRepository _interestRepository;

        public InterestsController(IInterestRepository interestRepository)
        {
            _interestRepository = interestRepository;
        }

        [HttpGet(Name = "Get All Interests")]
        public async Task<ActionResult<IEnumerable<GetInterests>>> GetInterests()
        {
            var interests = await _interestRepository.GetAllInterestsDetailsAsync();

            // Om inga intressen hittades
            if (interests == null || !interests.Any())
            {
                return NotFound(new { message = "Inga intressen hittades." });
            }

            return Ok(interests);
        }

        [HttpGet("{id}", Name = "Get Interest By Id")]
        public async Task<ActionResult<GetInterests>> GetInterestById(int id)
        {
            var interest = await _interestRepository.GetInterestDetailsAsync(id);

            if (interest == null)
            {
                return NotFound(new { message = "Intresset hittades inte i databasen." });
            }

            var interestDto = new GetInterests
            {
                Name = interest.Name,
                Description = interest.Description,
                Persons = interest.Persons?.Select(p => new GetPersonsFromInterest
                {
                    Name = p.Name
                }).ToList() ?? new List<GetPersonsFromInterest>(),  // Om Persons är null, returnera en tom lista

                Links = interest.Links?.Select(l => new GetLinksFromInterest
                {
                    URL = l.URL,
                    Description = l.Description,
                }).ToList() ?? new List<GetLinksFromInterest>()  // Om Links är null, returnera en tom lista
            };

            return Ok(interestDto);
        }

        [HttpPost(Name = "Create Interest")]
        public async Task<ActionResult> AddInterest(AddInterestRequest newInterest)
        {
            // Skapa ett nytt Interest objekt
            var interestEntity = new Interest
            {
                InterestName = newInterest.InterestName,
                Description = newInterest.Description,
                Links = new List<Link>(),  // Säkerställ att Links är initialiserad
                Persons = new List<Person>() // Säkerställ att Persons är initialiserad
            };

            // Lägg till det nya intresset i databasen
            await _interestRepository.AddAsync(interestEntity);
            await _interestRepository.SaveAsync();

            // Hämta länkar baserat på de angivna LinkIds från repositoryt
            if (newInterest.LinkIds != null && newInterest.LinkIds.Any())
            {
                var links = await _interestRepository.GetLinksByIdsAsync(newInterest.LinkIds);
                // Lägg till varje länk individuellt
                foreach (var link in links)
                {
                    interestEntity.Links.Add(link); // Lägger till länk till den initialiserade Links-samlingen
                }
            }

            // Hämta personer baserat på de angivna PersonIds från repositoryt
            if (newInterest.PersonIds != null && newInterest.PersonIds.Any())
            {
                var persons = await _interestRepository.GetPersonsByIdsAsync(newInterest.PersonIds);
                // Lägg till varje person individuellt
                foreach (var person in persons)
                {
                    interestEntity.Persons.Add(person); // Lägger till person till den initialiserade Persons-samlingen
                }
            }

            // Spara ändringarna i databasen
            await _interestRepository.SaveAsync();

            // Skapa en DTO för att returnera intresset
            var interestDto = new GetInterests
            {
                Name = interestEntity.InterestName,
                Description = interestEntity.Description,
                Persons = interestEntity.Persons.Select(p => new GetPersonsFromInterest
                {
                    Name = $"{p.FirstName} {p.LastName}"
                }).ToList(),
                Links = interestEntity.Links.Select(l => new GetLinksFromInterest
                {
                    URL = l.Url,
                    Description = l.Description,
                }).ToList()
            };

            // Returnera det nyskapade intresset
            return CreatedAtAction(nameof(GetInterestById), new { id = interestEntity.Id }, interestDto);
        }


        [HttpDelete("{id}", Name = "Delete Interest By Id")]
        public async Task<ActionResult> DeleteInterest(int id)
        {
            var interest = await _interestRepository.GetByIdAsync(id);

            if (interest == null)
            {
                return NotFound(new { message = "Intresset hittades inte i databasen." });
            }

            _interestRepository.Delete(interest);
            await _interestRepository.SaveAsync();

            return Ok(new { message = $"{interest.InterestName} har tagits bort från databasen." });
        }

    }
}
