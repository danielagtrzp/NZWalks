namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        //Created because I don´t want to received the ID in the request
        //If I want to avoid the update of a property I can delete It here, so client can not change it
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
