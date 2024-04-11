using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace Capstone_api.Models
{
    public class Person
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string FName { get; set; } = "";
        [Required]
        public string LName { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string Title { get; set; } = "";
        [Required]
        public string CID { get; set; } = "";
        [Required]
        public string AccessCode { get; set; } = "";
    }
}
