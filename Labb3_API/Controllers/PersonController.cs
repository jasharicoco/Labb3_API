using Labb3_API.Models;
using Labb3_API.Models.DTOs;
using Labb3_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Labb3_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet(Name = "Get All Persons")]
        public async Task<ActionResult<IEnumerable<GetPersons>>> GetPersons()
        {
            var persons = await _personRepository.GetAllPersonsDetailsAsync();
            return Ok(persons);
        }

        [HttpGet("{id}", Name = "Get Person By Id")]
        public async Task<ActionResult<GetPersons>> GetPersonById(int id)
        {
            var person = await _personRepository.GetPersonDetailsAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost(Name = "Add Person")]
        public async Task<ActionResult> AddPerson(AddPersonRequest newPerson)
        {
            var createdPerson = await _personRepository.CreatePersonWithInterestsAndLinksAsync(newPerson);

            var personDto = await _personRepository.GetPersonDetailsAsync(createdPerson.Id);

            return CreatedAtAction(nameof(GetPersonById), new { id = createdPerson.Id }, personDto);
        }

        [HttpDelete("{id}", Name = "Delete Person")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            if (person == null)
            {
                return NotFound(new { message = "Personen hittades inte i databasen." });
            }

            _personRepository.Delete(person);
            await _personRepository.SaveAsync();

            return Ok(new { message = $"{person.FirstName} {person.LastName} har tagits bort från databasen." });
        }

        [HttpGet("{id}/interests")]
        public async Task<IActionResult> GetInterestsForPerson(int id)
        {
            var interests = await _personRepository.GetInterestsByPersonIdAsync(id);
            if (interests == null) return NotFound();
            return Ok(interests);
        }

        [HttpGet("{id}/links")]
        public async Task<IActionResult> GetLinksForPerson(int id)
        {
            var links = await _personRepository.GetLinksByPersonIdAsync(id);
            if (links == null) return NotFound();
            return Ok(links);
        }

        [HttpPut("{personId}/interest/{interestId}")]
        public async Task<IActionResult> AddInterestToPerson(int personId, int interestId)
        {
            var result = await _personRepository.AddInterestToPersonWithConfirmationAsync(personId, interestId);

            if (result == null)
                return BadRequest("Personen eller intresset hittades ej - eller så är de redan kopplade.");

            var (personName, interestName) = result.Value; // Dekonstruera tuple korrekt här

            return Ok($"{interestName} har lagts till för {personName}");
        }

    }

}
