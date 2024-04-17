using System.ComponentModel.DataAnnotations;

namespace Capstone_api.Models
{
    public class AccessLog
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int LockNum { get; set; }
        [Required]
        public DateTime AccesTime { get; set; }
        [Required]
        public int CID { get; set; }
        [Required]
        public bool Accepted { get; set; }
    }
}
