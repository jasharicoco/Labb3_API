using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Labb3_API.Models.DTOs
{
    public class GetPersons
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [EmailAddress]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Email { get; set; }

        [Phone]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Telephone { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<GetInterestsFromPerson> Interests { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<GetLinksFromPerson> Links { get; set; }

    }
}
