using System.ComponentModel.DataAnnotations;

namespace Capstone_api.Models
{
    public class RequestLog
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int LockNum { get; set; }
        [Required]
        public string AccessCode { get; set; }
        [Required]
        public DateTime DateRequested { get; set; }
        [Required]
        public DateTime DateApproved { get; set; }
        [Required]
        public int AdvisorID { get; set; }
     
    }
}
