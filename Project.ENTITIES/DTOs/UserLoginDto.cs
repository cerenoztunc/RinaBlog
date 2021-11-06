using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.DTOs
{
    public class UserLoginDto
    {
        
        [DisplayName("E-Posta")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(10, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
