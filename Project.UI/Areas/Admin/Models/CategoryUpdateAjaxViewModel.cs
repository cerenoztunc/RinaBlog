using Project.ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.Models
{
    public class CategoryUpdateAjaxViewModel
    {
        public CategoryUpdateDto CategoryUpdateDto { get; set; }
        public string CategoryUpdatePartial { get; set; } //CategoryAddDto ile gidecek bir property.. Post işlemi yaptığımızda ajax ile bu post işlemi sonucunda bize CategoryAddPartial geliyor olacak..Yani modelin son durumu...
        public CategoryDto CategoryDto { get; set; }
    }
}
