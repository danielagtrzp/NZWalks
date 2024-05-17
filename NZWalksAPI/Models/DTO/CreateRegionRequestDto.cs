using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class CreateRegionRequestDto
    {
        [Required]
        [Range(0, 3)]
        public string Code { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public string? RegionImgUrl { get; set; }
    }
}
