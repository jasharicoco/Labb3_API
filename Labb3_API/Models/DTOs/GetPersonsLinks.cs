namespace Labb3_API.Models.DTOs
{
    public class GetPersonsLinks
    {
        public string Name { get; set; }
        public List<GetLinksFromPerson> Links { get; set; }
    }
}
