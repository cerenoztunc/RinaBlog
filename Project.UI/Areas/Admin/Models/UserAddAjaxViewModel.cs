using Project.ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.Models
{
    public class UserAddAjaxViewModel
    {
        public UserAddDto UserAddDto { get; set; }
        public string UserAddPartial { get; set; } //UserAddDto ile gidecek bir property.. Post işlemi yaptığımızda ajax ile bu post işlemi sonucunda bize UserAddPartial geliyor olacak..Yani modelin son durumu...
        public UserDto UserDto { get; set; }
    }
}
