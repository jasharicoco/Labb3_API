namespace Labb3_API.Models.DTOs
{
    public class GetPersonsInterests
    {
        public string Name { get; set; }
        public List<GetInterestsFromPerson> Interests { get; set; }

    }
}
