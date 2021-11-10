﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Utilities
{
    public static class Messages
    {
        public static class Category
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiçbir kategori bulunamadı.";
                return "Böyle bir kategori bulunamadı.";
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
        }
        public static class Article
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiçbir makale bulunamadı.";
                return "Böyle bir makale bulunamadı.";
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
        }
    }
}