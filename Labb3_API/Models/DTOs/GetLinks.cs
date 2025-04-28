using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Labb3_API.Models.DTOs
{
    public class GetLinks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public string Description { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<GetInterestsFromLink>? Interests { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<GetPersonsFromLink>? Persons { get; set; }

    }
}
