using System.Text.Json.Serialization;

namespace Labb3_API.Models.DTOs
{
    public class GetInterestsFromPerson
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<GetLinksFromPerson> Links { get; set; }

    }
}
