using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnHub.Models
{
    public class InstructorApplication
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Expertise { get; set; } // مجال الخبرة

        [Required]
        [StringLength(2000, MinimumLength = 50)]
        public string Bio { get; set; } // السيرة الذاتية أو الخبرة 

        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        [Required]
        public bool IsApproved { get; set; } = false;

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}