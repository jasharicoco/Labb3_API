using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Labb3_API.Models
{
    public class Link
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name name cannot be longer than 20 characters.")]
        public string Name { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        [Required]
        public string Description { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Person> Persons { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Interest> Interests { get; set; }

    }
}
