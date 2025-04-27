using System.ComponentModel.DataAnnotations;

namespace Labb3_API.Models.DTOs
{
    public class AddPersonRequest
    {
        public string Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? Telephone { get; set; }
        public List<int> InterestIds { get; set; }
        public List<int> LinkIds { get; set; }

    }
}
