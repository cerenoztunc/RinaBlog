using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Concrete
{
    public class AboutUsPageInfo
    {
        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MaxLength(150, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        public string Header { get; set; }
        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MaxLength(1500, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        public string Content { get; set; }
        [DisplayName("Seo Açıklama")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        public string SeoDescription { get; set; }
        [DisplayName("Seo Etiketleri")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        public string SeoTags { get; set; }
        [DisplayName("Seo Yazar")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MaxLength(60, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        public string SeoAuthor { get; set; }

    }
}
