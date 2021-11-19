using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.DTOs
{
    public class EmailSendDto
    {
        [DisplayName("İsminiz")]
        [Required(ErrorMessage ="{0} alanı zorunludur.")]
        [MaxLength(60, ErrorMessage ="{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string Name { get; set; }
        [DisplayName("E-posta Adresiniz")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [MaxLength(100, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(10, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string Email { get; set; }
        [DisplayName("Konu")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [MaxLength(125, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string Subject { get; set; }
        [DisplayName("Mesajınız")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [MaxLength(1500, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(20, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string Message { get; set; }
    }
}
