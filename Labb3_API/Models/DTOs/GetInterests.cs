using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Labb3_API.Models.DTOs
{
    public class GetInterests
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<GetPersonsFromInterest>? Persons { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<GetLinksFromInterest>? Links { get; set; }

    }
}
