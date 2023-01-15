using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class Trener
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Минимальное кол-во символов - 3, максимальное - 100")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Минимальное кол-во символов - 3, максимальное - 100")]
        public string SecondName { get; set; }
        [Required(ErrorMessage = "Укажите телефон")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Минимальное кол-во символов - 6, максимальное - 100")]
        [RegularExpression("^((8|\\+7)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$", ErrorMessage ="Введите корректный номер телефона")]
        public string Phone { get; set; }
    }
}   