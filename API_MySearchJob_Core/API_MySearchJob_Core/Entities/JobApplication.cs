namespace API_MySearchJob_Core.Entities
{
    public class JobApplication
    {
        public long Id { get; set; }
        public DateTime DatePublication { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string JobName { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string ApplicationPage { get; set; } = string.Empty;
        public decimal Salary { get; set; } 
        public string Benefits { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public string Functions { get; set; } = string.Empty;
        public string Softskills { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public long JobModalityId { get; set; }
        public long JobPlaceId { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

    }
}
