using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.DTOs
{
    public class UserPasswordChangeDto
    {
        
        [DisplayName("Mevcut Şifreniz")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [DisplayName("Yeni Şifreniz")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DisplayName("Yeni Şifrenizin Tekrarı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Şifre alanları birbiri ile uyuşmuyor!")]
        public string RepeatPassword { get; set; }
    }
}
