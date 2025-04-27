using Labb3_API.Models;
using Labb3_API.Models.DTOs;
using Labb3_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Labb3_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly ILinkRepository _linkRepository;

        public LinksController(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        [HttpGet(Name = "Get All Links")]
        public async Task<ActionResult<IEnumerable<GetLinks>>> GetInterests()
        {
            var interests = await _linkRepository.GetAllLinksDetailsAsync();

            // Om inga intressen hittades
            if (interests == null || !interests.Any())
            {
                return NotFound(new { message = "Inga intressen hittades." });
            }

            return Ok(interests);
        }

        [HttpGet("{id}", Name = "Get Link By Id")]
        public async Task<ActionResult<GetLinks>> GetLinkById(int id)
        {
            var link = await _linkRepository.GetLinkDetailsAsync(id);

            if (link == null)
            {
                return NotFound(new { message = "Länken hittades inte i databasen." });
            }

            var linkDto = new GetLinks
            {
                Url = link.Url,
                Description = link.Description,
                Persons = link.Persons?.Select(p => new GetPersonsFromLink
                {
                    Name = p.Name
                }).ToList() ?? new List<GetPersonsFromLink>(),  // Om Persons är null, returnera en tom lista

                Interests = link.Interests?.Select(l => new GetInterestsFromLink
                {
                    Name = l.Name,
                    Description = l.Description,
                }).ToList() ?? new List<GetInterestsFromLink>()
            };

            return Ok(linkDto);
        }

        [HttpPost(Name = "Create Link")]
        public async Task<ActionResult> AddLink(AddLinkRequest newLink)
        {
            // Skapa ett nytt Link objekt och säkerställ att Interests och Persons är initialiserade
            var linkEntity = new Link
            {
                Url = newLink.URL,
                Description = newLink.Description,
                Interests = new List<Interest>(), // Initialisera Interests
                Persons = new List<Person>() // Initialisera Persons
            };

            // Lägg till den nya länken i databasen
            await _linkRepository.AddAsync(linkEntity);
            await _linkRepository.SaveAsync();

            // Hämta intressen baserat på de angivna InterestIds
            if (newLink.InterestIds != null && newLink.InterestIds.Any())
            {
                var interests = await _linkRepository.GetInterestsByIdsAsync(newLink.InterestIds);
                // Koppla varje intresse till länken
                foreach (var interest in interests)
                {
                    linkEntity.Interests.Add(interest); // Lägg till intresse till länken
                }
            }

            // Hämta personer baserat på de angivna PersonIds
            if (newLink.PersonIds != null && newLink.PersonIds.Any())
            {
                var persons = await _linkRepository.GetPersonsByIdsAsync(newLink.PersonIds);
                // Koppla varje person till länken
                foreach (var person in persons)
                {
                    linkEntity.Persons.Add(person); // Lägg till person till länken
                }
            }

            // Spara ändringarna i databasen
            await _linkRepository.SaveAsync();

            // Skapa en DTO för att returnera länken
            var linkDto = new GetLinks
            {
                Url = linkEntity.Url,
                Description = linkEntity.Description,
                Persons = linkEntity.Persons.Select(p => new GetPersonsFromLink
                {
                    Name = $"{p.FirstName} {p.LastName}"
                }).ToList(),
                Interests = linkEntity.Interests.Select(i => new GetInterestsFromLink
                {
                    Name = i.InterestName,
                    Description = i.Description
                }).ToList(),
            };

            // Returnera den nyskapade länken
            return CreatedAtAction(nameof(GetLinkById), new { id = linkEntity.Id }, linkDto);
        }


        [HttpDelete("{id}", Name = "Delete Link By Id")]
        public async Task<ActionResult> DeleteLink(int id)
        {
            var link = await _linkRepository.GetByIdAsync(id);

            if (link == null)
            {
                return NotFound(new { message = "Länken hittades inte i databasen." });
            }

            _linkRepository.Delete(link);
            await _linkRepository.SaveAsync();

            return Ok(new { message = $"{link.Url} har tagits bort från databasen." });
        }

    }
}
