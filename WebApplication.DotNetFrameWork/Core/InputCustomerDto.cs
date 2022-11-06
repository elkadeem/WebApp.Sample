using System.ComponentModel.DataAnnotations;

namespace WebApplication.DotNetFrameWork.Core
{
    public class InputCustomerDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }
    }
}