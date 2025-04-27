namespace Labb3_API.Models.DTOs
{
    public class AddInterestRequest
    {
        public string InterestName { get; set; }
        public string Description { get; set; }
        public List<int> LinkIds { get; set; }
        public List<int> PersonIds { get; set; }

    }

}
