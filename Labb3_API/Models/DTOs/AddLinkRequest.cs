namespace Labb3_API.Models.DTOs
{
    public class AddLinkRequest
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public List<int> InterestIds { get; set; }
        public List<int> PersonIds { get; set; }

    }
}
