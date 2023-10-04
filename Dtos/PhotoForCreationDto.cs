namespace CityGuide.Dtos
{
    public class PhotoForCreationDto
    {
        public PhotoForCreationDto() {
            CreatedAt = DateTime.Now;
        }
        public string Url { get; set; }
        public string Description { get; set; }
        public IFormFile  formFile { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string  PublicId {  get; set; }

    }
}
