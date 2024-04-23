namespace Capstone_api.Models
{
    public class QuickStatistics
    {
        public int ID { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int NumFailedLastDay { get; set; }
        public string HasPendingRequest { get; set; }
        public int NumAccessesLastHour { get; set; }
    }
}
