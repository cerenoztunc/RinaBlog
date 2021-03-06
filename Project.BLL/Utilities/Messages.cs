using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Utilities
{
    public static class Messages
    {
        public static class General
        {
            public static string ValidationError()
            {
                return $"Bir veya daha fazla validasyon hatası ile karşılaşıldı.";
            }
        }
        public static class Category
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiçbir kategori bulunamadı.";
                return "Böyle bir kategori bulunamadı.";
            }
            public static string NotFoundById(int categoryID)
            {
                return $"{categoryID} kategori koduna ait bir kategori bulunamadı.";
            }
            public static string AddAsync(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla eklenmiştir.";
            }
            public static string UpdateAsync(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla güncellenmiştir.";
            }
            public static string DeleteAsync(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla silinmiştir.";
            }
            public static string HardDeleteAsync(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla veritabanından silinmiştir.";
            }
            public static string UndoDelete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla arşivden getirilmiştir.";
            }
        }
        public static class Article
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiçbir makale bulunamadı.";
                return "Böyle bir makale bulunamadı.";
            }
            public static string NotFoundById(int articleID)
            {
                return $"{articleID} makale koduna ait bir makale bulunamadı.";
            }
            public static string AddAsync(string articleName)
            {
                return $"{articleName} adlı kategori başarıyla eklenmiştir.";
            }
            public static string UpdateAsync(string articleName)
            {
                return $"{articleName} adlı kategori başarıyla güncellenmiştir.";
            }
            public static string DeleteAsync(string articleName)
            {
                return $"{articleName} adlı kategori başarıyla silinmiştir.";
            }
            public static string HardDeleteAsync(string articleName)
            {
                return $"{articleName} adlı kategori başarıyla veritabanından silinmiştir.";
            }
            public static string UndoDelete(string articleName)
            {
                return $"{articleName} adlı kategori başarıyla arşivden geri getirilmiştir";
            }
            public static string IncreaseViewCount(string articleTitle)
            {
                return $"{articleTitle} başlıklı makalenin okunma sayısı başarıyla arttırılmıştır.";
            }
        }
        public static class Comment
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir yorum bulunamadı.";
                return "Böyle bir yorum bulunamadı.";
            }
            public static string Approve(int commentId)
            {
                return $"{commentId} no'lu yorum başarıyla onaylanmıştır.";
            }

            public static string Add(string createdByName)
            {
                return $"Sayın {createdByName}, yorumunuz başarıyla eklenmiştir.";
            }

            public static string Update(string createdByName)
            {
                return $"{createdByName} tarafından eklenen yorum başarıyla güncellenmiştir.";
            }
            public static string Delete(string createdByName)
            {
                return $"{createdByName} tarafından eklenen yorum başarıyla silinmiştir.";
            }
            public static string HardDelete(string createdByName)
            {
                return $"{createdByName} tarafından eklenen yorum başarıyla veritabanından silinmiştir.";
            }
            public static string UndoDelete(string createdByName)
            {
                return $"{createdByName} tarafından eklenen yorum başarıyla arşivden getirilmiştir.";
            }
        }
        public static class User
        {
            public static string NotFoundById(int userId)
            {
                return $"{userId} kullanıcı koduna ait bir kullanıcı bulunamadı.";
            }
        }
    }
}
