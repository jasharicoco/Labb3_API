namespace Labb3_API.Models.DTOs
{
    public class UpdatePersonRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int[]? InterestIds { get; set; }
        public int[]? LinkIds { get; set; }

    }
}
