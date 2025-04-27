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

    }

}
