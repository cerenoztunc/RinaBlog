using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.Models
{
    public class ArticleUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        public string Title { get; set; }

        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MinLength(20, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        public string Content { get; set; }
        [DisplayName("Küçük Resim")]
        public string Thumbnail { get; set; }

        [DisplayName("Küçük Resim Ekle")]
        public IFormFile ThumbnailFile { get; set; }

        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Yazar Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MaxLength(50, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(0, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        public string SeoAuthor { get; set; }

        [DisplayName("Makale Açıklaması")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MaxLength(150, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(0, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        public string SeoDescription { get; set; }

        [DisplayName("Makale Etiketleri")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden fazla olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olamaz!")]
        public string SeoTags { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        public int CategoryID { get; set; }
        [Required]
        public int UserId { get; set; }
        public IList<Category> Categories { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        public bool IsActive { get; set; }
    }
}
