namespace NZWalks.API.Models.DTO
{
    public class CreateRegionRequestDto
    {
        //Created because I don´t want to received the ID in the request
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImgUrl { get; set; }
    }
}
