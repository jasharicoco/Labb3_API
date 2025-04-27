using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Labb3_API.Models
{
    public class Interest
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Interest name cannot be longer than 50 characters.")]
        public string InterestName { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string Description { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Person>? Persons { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Link>? Links { get; set; }

    }
}
