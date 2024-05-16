namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        //Created because I don´t want to received the ID in the request
        //If I want to avoid the update of a property I can delete It here, so client can not change it
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImgUrl { get; set; }
    }
}
