namespace CityGuide.Dtos
{
    public class PhotoForReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Discription { get; set; }
        public DateTime dateTime  { get; set; }
        public  bool  IsMain { get; set; }
        public string PublicId { get; set; }
    }
}
