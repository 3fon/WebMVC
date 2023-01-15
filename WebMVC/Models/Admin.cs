using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Минимальное кол-во символов - 6, максимальное - 100")]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "Укажите пароль")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Минимальное кол-во символов - 6, максимальное - 100")]
        public string Password { get; set; }

    }

}
