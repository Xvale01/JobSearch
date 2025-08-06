namespace Web_MySearchJob_Core.Entities
{
    public class JobPlace
    {
        public long Id { get; set; }
        public string country { get; set; } = string.Empty;
        public string province { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
    }
}
